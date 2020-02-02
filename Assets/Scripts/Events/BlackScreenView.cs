using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Events
{
    public class BlackScreenView : AbstractBasicTriggerView
    {
        [SerializeField] private Image _muteIcon;

        private void Awake()
        {
            Screen.brightness = 0f;
            Camera.main.backgroundColor = Color.black;

            var serialDisposable = new SerialDisposable().AddTo(gameObject);
            var inputDetection = GameUtility.CreateInputDetection(InputDetection.ScreenBrightnessUp, serialDisposable, null);

            inputDetection.Triggered.Subscribe(_ => OnComplete()).AddTo(gameObject);
            
            SetupMuteIcon();
        }

        private void SetupMuteIcon()
        {
            var serialDisposable = new SerialDisposable().AddTo(gameObject);
            var inputDetection = GameUtility.CreateInputDetection(InputDetection.VolumeUp, serialDisposable, null);

            var isAnimating = true;
            var isIncreasing = false;

            var alphaValue = Observable.EveryUpdate().Where(_ => isAnimating).Select(_ => isIncreasing ? 1.0f : -1.0f)
                .Scan(1.0f, (oldValue, newValue) => Mathf.Clamp(oldValue + newValue * 0.03f, 0.0f, 1.0f));

            alphaValue.Where(alpha => alpha < 0.1f || alpha > 0.9f).Subscribe(alpha => isIncreasing = alpha < 0.1f).AddTo(gameObject);
            alphaValue.Subscribe(alpha => _muteIcon.color = new Color(1, 1, 1, alpha)).AddTo(gameObject);

            inputDetection.Triggered.Subscribe(_ =>
            {
                isAnimating = false;
                _muteIcon.gameObject.SetActive(false);
            }).AddTo(gameObject);
        }
    }
}
