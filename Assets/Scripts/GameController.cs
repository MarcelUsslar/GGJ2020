using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text _debugText;

    private SerialDisposable _detectionDisposable;
    private LoopConfig _config;
    private int _eventIndex = 0;

    private void Awake()
    {
        _detectionDisposable = new SerialDisposable().AddTo(gameObject);
        _config = GameUtility.LoadConfig();

        InitAllInputDetections();

        SpawnEvent();
    }

    private static void InitAllInputDetections()
    {
        var disposable = new SerialDisposable();
        foreach (var detection in EnumHelper<InputDetection>.Iterator)
        {
            GameUtility.CreateInputDetection(detection, disposable, null);
        }

        disposable.Dispose();
    }

    private void SpawnEvent()
    {
        if (_eventIndex >= _config.EventCount)
        {
            Application.Quit();
            return;
        }
        
        var eventPrefab = Instantiate(_config.Prefab(_eventIndex), transform);

        GameUtility.Log(_debugText, "Create Input Detection for {0}", eventPrefab.name);
        
        _detectionDisposable.Disposable = eventPrefab.Trigger
            .Subscribe(trigger =>
            {
                GameUtility.Log(_debugText, "{0} condition met.", eventPrefab.name);

                Destroy(eventPrefab.gameObject);

                _eventIndex++;
                SpawnEvent();
            });
    }
}