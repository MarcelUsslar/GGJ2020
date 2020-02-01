using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Events
{
    public class BlueScreenView : AbstractBasicTriggerView
    {
        [SerializeField] private Button _screenButton;

        private void Awake()
        {
            _screenButton.OnClickAsObservable().Subscribe(_ => OnComplete()).AddTo(gameObject);
        }
    }
}
