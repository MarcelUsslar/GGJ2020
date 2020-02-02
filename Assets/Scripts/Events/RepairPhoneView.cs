using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Events
{
    public class RepairPhoneView : AbstractBasicTriggerView
    {
        [SerializeField] private Button _repairButton;

        private void Awake()
        {
            _repairButton.OnClickAsObservable().Subscribe(_ => OnComplete()).AddTo(gameObject);
        }
    }
}
