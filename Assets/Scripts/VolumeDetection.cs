using System;
using UniRx;
using UnityEngine;

public class VolumeDetection : IInputDetection
{
    private const string K_audioService = "audio";

    private const int K_streamVoiceCall = 0;
    private const int K_streamSystem = 1;
    private const int K_streamRing = 2;
    private const int K_streamMusic = 3;
    private const int K_streamAlarm = 4;
    private const int K_streamNotification = 5;
    private const int K_streamDtmf = 8;

    public IObservable<float> CurrentVolume { get; }

    public IObservable<Unit> Triggered { get; }

    public VolumeDetection(bool increaseVolume)
    {
        var initialVolume = GetCurrentMusicVolume();

        CurrentVolume = Observable.EveryUpdate()
            .Select(_ => GetCurrentMusicVolume())
            .DistinctUntilChanged();

        Triggered = CurrentVolume.Select(volume => increaseVolume ? volume > initialVolume : volume < initialVolume)
            .Where(active => active).Select(_ => Unit.Default).Take(1);
    }

    public float GetCurrentMusicVolume()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        var application = new AndroidJavaObject("com.unity3d.player.UnityPlayer");
        var context = application.GetStatic<AndroidJavaObject>("currentActivity");

        var max = 0;
        var current = 0;
        using (var audioManager = context.Call<AndroidJavaObject>("getSystemService", K_audioService))
        {
            max = audioManager.Call<int>("getStreamMaxVolume", K_streamMusic);
            current = audioManager.Call<int>("getStreamVolume", K_streamMusic);
        }

        //return current;
        return (float)current / (float)max;
#endif
        return 0.0f;
    }
}