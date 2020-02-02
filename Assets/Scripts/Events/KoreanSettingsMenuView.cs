using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Events
{
    public class KoreanSettingsMenuView : AbstractBasicTriggerView
    {
        [SerializeField] private Button _settingButton;
        [Space]
        [SerializeField] private Image _settingsBackground;
        [SerializeField] private Image _koreanSettingsText;
        [Space]
        [SerializeField] private Image _korean1;
        [SerializeField] private Image _korean2;
        [SerializeField] private Image _korean3;
        
        private float _generalAlpha = 0;
        private float _koreanAlpha = 0;

        private void Awake()
        {
            _settingButton.transform.position = AndroidHomeScreenView.DownLoadButtonPosition;
            
            _settingButton.OnClickAsObservable().Subscribe(_ => OpenSettings()).AddTo(gameObject);
        }

        private void OpenSettings()
        {
            Destroy(_settingButton.gameObject);

            var serialDisposable = new SerialDisposable().AddTo(gameObject);
            var inputDetection = GameUtility.CreateInputDetection(InputDetection.Disconnected, serialDisposable, null);

            inputDetection.Triggered.Subscribe(_ => OnComplete()).AddTo(gameObject);

            Observable.EveryUpdate().StartWith(0).Subscribe(_ => OpenAnimation()).AddTo(gameObject);
        }
        
        private void OpenAnimation()
        {
            _settingsBackground.color = new Color(1, 1, 1, _generalAlpha);
            _koreanSettingsText.color = new Color(1, 1, 1, _koreanAlpha);
            _korean1.color = new Color(1, 1, 1, _koreanAlpha - 0.5f);
            _korean2.color = new Color(1, 1, 1, _koreanAlpha - 0.8f);
            _korean3.color = new Color(1, 1, 1, _koreanAlpha - 1.1f);
            
            _generalAlpha += 0.1f;
            _koreanAlpha += 0.05f;
        }
    }
}
