using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private float _delay;
    [Space]
    [SerializeField] private LoopConfig _config;
    [SerializeField] private Text _debugText;
    [SerializeField] private Text _stateText;

    private SerialDisposable _detectionDisposable;
    private SerialDisposable _stateDisposable;
    private SerialDisposable _delayDisposable;
    private int _eventIndex = 0;

    private void Start()
    {
        _detectionDisposable = new SerialDisposable().AddTo(gameObject);
        _stateDisposable = new SerialDisposable().AddTo(gameObject);
        _delayDisposable = new SerialDisposable().AddTo(gameObject);

        SpawnEvent();
    }

    private void SpawnEvent()
    {
        if (_eventIndex >= _config.EventCount)
        {
            Application.Quit();
            return;
        }

        var inputDetectionTrigger = _config.Trigger(_eventIndex);
        
        GameUtility.Log(_debugText, "Create Input Detection for {0}", inputDetectionTrigger);

        var eventPrefab = Instantiate(_config.Prefab(_eventIndex), transform);
        var inputDetection = GameUtility.CreateInputDetection(inputDetectionTrigger, eventPrefab, _stateDisposable, _stateText, _config);

        _detectionDisposable.Disposable = inputDetection.Triggered
            .Subscribe(trigger =>
            {
                Destroy(eventPrefab);

                GameUtility.Log(_debugText, "{0} condition met.", inputDetectionTrigger);

                _delayDisposable.Disposable = Observable.Timer(TimeSpan.FromSeconds(_delay))
                    .Subscribe(delay =>
                    {
                        _eventIndex++;
                        SpawnEvent();
                    });
            });
    }
}