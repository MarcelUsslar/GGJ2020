using System;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private float _delay;
    [Space]
    [SerializeField] private LoopConfig _config;
    [SerializeField] private Text _debugText;
    [SerializeField] private Text _stateText;

    private SerialDisposable _detectionDisposable;
    private SerialDisposable _stateDisposable;
    private SerialDisposable _delayDisposable;
    private int _eventIndex = 0;

    private void Start()
    {
        _detectionDisposable = new SerialDisposable().AddTo(gameObject);
        _stateDisposable = new SerialDisposable().AddTo(gameObject);
        _delayDisposable = new SerialDisposable().AddTo(gameObject);

        SpawnEvent();
    }

    private void SpawnEvent()
    {
        if (_eventIndex >= _config.EventCount)
        {
            Application.Quit();
            return;
        }

        var inputDetectionTrigger = _config.Trigger(_eventIndex);
        
        Log(_debugText, "Create Input Detection for {0}", inputDetectionTrigger);

        var eventPrefab = Instantiate(_config.Prefab(_eventIndex), transform);
        var inputDetection = CreateInputDetection(inputDetectionTrigger, eventPrefab);

        _detectionDisposable.Disposable = inputDetection.Triggered
            .Subscribe(trigger =>
            {
                Destroy(eventPrefab);

                Log(_debugText, "{0} condition met.", inputDetectionTrigger);

                _delayDisposable.Disposable = Observable.Timer(TimeSpan.FromSeconds(_delay))
                    .Subscribe(delay =>
                    {
                        _eventIndex++;
                        SpawnEvent();
                    });
            });
    }

    private IInputDetection CreateInputDetection(InputDetection inputDetection, GameObject eventPrefab)
    {
        switch (inputDetection)
        {
            case InputDetection.Battery:
                return CreateBatteryDetection();
            case InputDetection.Internet:
                return CreateInternetDetection(NetworkReachability.ReachableViaCarrierDataNetwork, NetworkReachability.ReachableViaLocalAreaNetwork);
            case InputDetection.Wifi:
                return CreateInternetDetection(NetworkReachability.ReachableViaLocalAreaNetwork);
            case InputDetection.MobileData:
                return CreateInternetDetection(NetworkReachability.ReachableViaCarrierDataNetwork);
            case InputDetection.Disconnected:
                return CreateInternetDetection(NetworkReachability.NotReachable);
            case InputDetection.Shake:
                Log(_stateText, "Waiting for shake");
                return new ShakeDetection(_config.ShakeThreshold);
            case InputDetection.ScreenBrightnessUp:
                return CreateScreenBrightnessDetection(brightness => brightness >= 1.0f);
            case InputDetection.ScreenBrightnessDown:
                return CreateScreenBrightnessDetection(brightness => brightness <= 0.5f);
            case InputDetection.VolumeUp:
                return CreateVolumeDetection(true);
            case InputDetection.VolumeDown:
                return CreateVolumeDetection(false);
            default:
                throw new ArgumentOutOfRangeException(nameof(inputDetection), inputDetection, null);
        }
    }

    private BatteryDetection CreateBatteryDetection()
    {
        var batteryDetection = new BatteryDetection(BatteryStatus.Charging, BatteryStatus.Full);
        _stateDisposable.Disposable = batteryDetection.BatteryStatus.Subscribe(status => Log(_stateText, "Status: {0}", status));

        return batteryDetection;
    }

    private InternetDetection CreateInternetDetection(params NetworkReachability[] allowedNetworkStates)
    {
        var internetDetection = new InternetDetection(allowedNetworkStates);
        _stateDisposable.Disposable = internetDetection.InternetState.Subscribe(status => Log(_stateText, "Status: {0}", status));

        return internetDetection;
    }
    
    private ScreenBrightnessDetection CreateScreenBrightnessDetection(Func<float, bool> hasReachedBrightness)
    {
        var screenBrightnessDetection = new ScreenBrightnessDetection(hasReachedBrightness);
        _stateDisposable.Disposable = screenBrightnessDetection.ScreenBrightness.Subscribe(brightness => Log(_stateText, "Status: {0}", brightness));
        
        return screenBrightnessDetection;
    }

    private VolumeDetection CreateVolumeDetection(bool increaseVolume)
    {
        var volumeDetection = new VolumeDetection(increaseVolume);
        _stateDisposable.Disposable = volumeDetection.CurrentVolume.Subscribe(volume => Log(_stateText, "Volume: {0}", volume));

        return volumeDetection;
    }

    [StringFormatMethod("message")]
    private static void Log(Text textField, string message, params object[] parameters)
    {
        textField.text = string.Format(message, parameters);
    }
}