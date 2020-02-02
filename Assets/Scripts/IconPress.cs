using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class IconPress : MonoBehaviour
{
    [SerializeField] private Button _button;

    private float _randomDirection;
    private float _randomRotation;
    private float _positionY = 25f;

    private bool _isClicked;

    private readonly Subject<Unit> _onClicked = new Subject<Unit>();
    public IObservable<Unit> OnClicked => _onClicked;

    private readonly Subject<Unit> _onDestroy = new Subject<Unit>();
    public IObservable<Unit> OnDestroy => _onDestroy;

    private void Start()
    {
        _randomDirection = Random.Range(-0.1f, 0.1f);
        _randomRotation = Random.Range(10, 20);

        if (Random.value > 0.5f)
            _randomRotation *= -1;

        _button.OnClickAsObservable()
            .Subscribe(_ =>
            {
                _onClicked.OnNext(Unit.Default);
                _isClicked = true;
            }).AddTo(gameObject);

        Observable.EveryUpdate().Where(_ => _isClicked).Subscribe(_ => Fall()).AddTo(gameObject);
    }
    
    private void Fall()
    {
        transform.Translate(new Vector3(_randomDirection, _positionY, 0), Space.World);
        _positionY -= 2f;

        transform.Rotate(new Vector3(0, 0, _randomRotation));

        if (transform.position.y < -5)
        {
            _onDestroy.OnNext(Unit.Default);
            Destroy(gameObject);
        }
    }
}