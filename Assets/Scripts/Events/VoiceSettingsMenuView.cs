using UniRx;

namespace Events
{
    public class VoiceSettingsMenuView : AbstractBasicTriggerView
    {
        private void Awake()
        {
            var serialDisposable = new SerialDisposable().AddTo(gameObject);
            var inputDetection = GameUtility.CreateInputDetection(InputDetection.MicrophoneLoudInput, serialDisposable, null);

            inputDetection.Triggered.Subscribe(_ => OnComplete()).AddTo(gameObject);
        }
    }
}