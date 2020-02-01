using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteraction : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void Start()
    {
        _button.OnClickAsObservable().Subscribe(_ => ExecuteMethod()).AddTo(gameObject);
    }

    private void ExecuteMethod()
    {
        Debug.Log("Button was clicked");
    }
}