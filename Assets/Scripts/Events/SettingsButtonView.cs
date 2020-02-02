using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Events
{
    public class SettingsButtonView : AbstractBasicTriggerView
    {
        [SerializeField] private Button _settingsButton;

        private void Awake()
        {
            _settingsButton.transform.position = AndroidHomeScreenView.DownLoadButtonPosition;
            _settingsButton.OnClickAsObservable().Subscribe(_ => OnComplete()).AddTo(gameObject);
        }
    }
}