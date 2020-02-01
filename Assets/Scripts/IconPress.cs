using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class IconPress : MonoBehaviour
{
    [SerializeField] private Button _button;

    private bool _isClicked;
    private float _randomDirection;
    private float _randomRotation;
    private float _positionY = 0.25f;

    public bool IsClicked => _isClicked;

    private void Start()
    {
        _randomDirection = Random.Range(-0.1f, 0.1f);
        _randomRotation = Random.Range(10, 20);

        if (Random.value > 0.5f)
            _randomRotation = _randomRotation * -1;

        _button.OnClickAsObservable().Subscribe(_ => _isClicked = true).AddTo(gameObject);
        Observable.EveryUpdate().Where(_ => _isClicked).Subscribe(_ => Fall()).AddTo(gameObject);
    }
    
    private void Fall()
    {
        transform.Translate(new Vector3(_randomDirection, _positionY, 0), Space.World);
        _positionY -= 0.02f;

        transform.Rotate(new Vector3(0, 0, _randomRotation));

        if (transform.position.y < -5)
            Destroy(gameObject);
    }
}