using UniRx;

namespace Events
{
    public class WindowsHomeScreenView : AbstractBasicTriggerView
    {
        private void Awake()
        {
            var serialDisposable = new SerialDisposable().AddTo(gameObject);
            var inputDetection = GameUtility.CreateInputDetection(InputDetection.Shake, serialDisposable, null);

            inputDetection.Triggered.Subscribe(_ => OnComplete()).AddTo(gameObject);
        }
    }
}
