using UniRx;

namespace Events
{
    public class WhiteScreenView : AbstractBasicTriggerView
    {
        private void Awake()
        {
            var serialDisposable = new SerialDisposable().AddTo(gameObject);
            var inputDetection = GameUtility.CreateInputDetection(InputDetection.ScreenBrightnessDown, serialDisposable, null);

            inputDetection.Triggered.Subscribe(_ => OnComplete()).AddTo(gameObject);
        }
    }
}