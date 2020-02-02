using System;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public static class GameUtility
{
    [StringFormatMethod("message")]
    public static void Log(Text textField, string message, params object[] parameters)
    {
        if (textField == null)
        {
            Debug.LogWarning(string.Format(message, parameters));
            return;
        }

        textField.text = string.Format(message, parameters);
    }

    public static LoopConfig LoadConfig()
    {
        return Resources.Load<LoopConfig>("LoopConfig");
    }

    public static IInputDetection CreateInputDetection(InputDetection inputDetection,
        SerialDisposable serialDisposable, Text stateText)
    {
        switch (inputDetection)
        {
            case InputDetection.Battery:
                return CreateBatteryDetection(serialDisposable, stateText);
            case InputDetection.Internet:
                return CreateInternetDetection(serialDisposable, stateText, NetworkReachability.ReachableViaCarrierDataNetwork, NetworkReachability.ReachableViaLocalAreaNetwork);
            case InputDetection.Wifi:
                return CreateInternetDetection(serialDisposable, stateText, NetworkReachability.ReachableViaLocalAreaNetwork);
            case InputDetection.MobileData:
                return CreateInternetDetection(serialDisposable, stateText, NetworkReachability.ReachableViaCarrierDataNetwork);
            case InputDetection.Disconnected:
                return CreateInternetDetection(serialDisposable, stateText, NetworkReachability.NotReachable);
            case InputDetection.Shake:
                Log(stateText, "Waiting for shake");
                return new ShakeDetection(LoadConfig().ShakeThreshold);
            case InputDetection.ScreenBrightnessUp:
                return CreateScreenBrightnessDetection(serialDisposable, stateText, brightness => brightness >= 0.95f);
            case InputDetection.ScreenBrightnessDown:
                return CreateScreenBrightnessDetection(serialDisposable, stateText, brightness => brightness <= 0.7f);
            case InputDetection.VolumeUp:
                return CreateVolumeDetection(serialDisposable, stateText, true);
            case InputDetection.VolumeDown:
                return CreateVolumeDetection(serialDisposable, stateText, false);
            case InputDetection.MicrophoneLoudInput:
                return CreateMicrophoneInputDetection(serialDisposable, stateText, inputVolume => inputVolume >= LoadConfig().RequiredVolume);
            case InputDetection.MicrophoneSilentInput:
                return CreateMicrophoneInputDetection(serialDisposable, stateText, inputVolume => inputVolume <= 0.0002f);
            default:
                throw new ArgumentOutOfRangeException(nameof(inputDetection), inputDetection, null);
        }
    }

    private static BatteryDetection CreateBatteryDetection(SerialDisposable serialDisposable, Text stateText)
    {
        var batteryDetection = new BatteryDetection(BatteryStatus.Charging, BatteryStatus.Full);
        serialDisposable.Disposable = batteryDetection.BatteryStatus.Subscribe(status => Log(stateText, "Status: {0}", status));

        return batteryDetection;
    }

    private static InternetDetection CreateInternetDetection(SerialDisposable serialDisposable, Text stateText,
        params NetworkReachability[] allowedNetworkStates)
    {
        var internetDetection = new InternetDetection(allowedNetworkStates);
        serialDisposable.Disposable = internetDetection.InternetState.Subscribe(status => Log(stateText, "Status: {0}", status));

        return internetDetection;
    }

    private static ScreenBrightnessDetection CreateScreenBrightnessDetection(SerialDisposable serialDisposable, Text stateText,
        Func<float, bool> hasReachedBrightness)
    {
        var screenBrightnessDetection = new ScreenBrightnessDetection(hasReachedBrightness);
        serialDisposable.Disposable = screenBrightnessDetection.ScreenBrightness.Subscribe(brightness => Log(stateText, "Status: {0}", brightness));

        return screenBrightnessDetection;
    }

    private static VolumeDetection CreateVolumeDetection(SerialDisposable serialDisposable, Text stateText, bool increaseVolume)
    {
        var volumeDetection = new VolumeDetection(increaseVolume);
        serialDisposable.Disposable = volumeDetection.CurrentVolume.Subscribe(volume => Log(stateText, "Volume: {0}", volume));

        return volumeDetection;
    }

    private static MicrophoneInputDetection CreateMicrophoneInputDetection(SerialDisposable serialDisposable, Text stateText, Func<float, bool> hasReachedInputVolume)
    {
        var microphoneInput = new MicrophoneInputDetection(hasReachedInputVolume);

        var disposable = new CompositeDisposable();
        serialDisposable.Disposable = disposable;

        microphoneInput.InputVolume.Subscribe(volume => Log(stateText, "Input Volume: {0}", volume)).AddTo(disposable);
        Disposable.Create(() => microphoneInput.Dispose()).AddTo(disposable);

        return microphoneInput;
    }
}