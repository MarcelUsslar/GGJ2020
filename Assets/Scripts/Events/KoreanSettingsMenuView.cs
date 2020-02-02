using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Events
{
    public class KoreanSettingsMenuView : AbstractBasicTriggerView
    {
        [SerializeField] private Button _settingButton;
        [SerializeField] private GameObject _androidBackground;
        [Space]
        [SerializeField] private Image _settingsBackground;
        [SerializeField] private Image _koreanSettingsText;
        [Space]
        [SerializeField] private Image _korean1;
        [SerializeField] private Image _korean2;
        [SerializeField] private Image _korean3;
        
        private float _generalAlpha = 0;
        private float _koreanAlpha = 0;

        private bool _isScalingUp = true;

        private void Awake()
        {
            var settingButtonTransform = _settingButton.transform;
            settingButtonTransform.position = AndroidHomeScreenView.DownLoadButtonPosition;
            
            _settingButton.OnClickAsObservable().Subscribe(_ => OpenSettings()).AddTo(gameObject);

            var buttonScale = Observable.EveryUpdate().Select(_ => _isScalingUp ? 0.05f : -0.02f).StartWith(0.0f)
                .Pairwise((oldValue, newValue) => oldValue + newValue).ToReadOnlyReactiveProperty().AddTo(gameObject);

            buttonScale.Where(scale => scale >= 0.4f).Take(1).Delay(TimeSpan.FromSeconds(0.2f))
                .Subscribe(_ => _isScalingUp = false).AddTo(gameObject);

            buttonScale.Subscribe(scale => transform.localScale = new Vector3(scale, scale, 1)).AddTo(gameObject);
        }

        private void OpenSettings()
        {
            _androidBackground.SetActive(false);

            var serialDisposable = new SerialDisposable().AddTo(gameObject);
            var inputDetection = GameUtility.CreateInputDetection(InputDetection.Disconnected, serialDisposable, null);

            inputDetection.Triggered.Subscribe(_ => OnComplete()).AddTo(gameObject);

            Observable.EveryUpdate().Subscribe(_ => OpenAnimation()).AddTo(gameObject);
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
