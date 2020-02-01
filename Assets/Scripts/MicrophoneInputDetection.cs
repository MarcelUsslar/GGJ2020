using System;
using System.Linq;
using UniRx;
using UnityEngine;

public class MicrophoneInputDetection : IInputDetection
{
    private const int K_sampleWindow = 128;

    private readonly string _device;
    private readonly AudioClip _clipRecord;

    public IObservable<float> InputVolume { get; }

    public IObservable<Unit> Triggered { get; }

    public MicrophoneInputDetection(Func<float, bool> hasReachedInputVolume)
    {
        _device = Microphone.devices[0];
        _clipRecord = Microphone.Start(_device, true, 999, 44100);

        InputVolume = Observable.EveryUpdate().Select(_ => GetMaxInput()).DistinctUntilChanged();

        Triggered = InputVolume.Select(hasReachedInputVolume).Where(active => active)
            .Select(_ => Unit.Default).Take(1);
    }

    public void Dispose()
    {
        Microphone.End(_device);
    }

    private float GetMaxInput()
    {
        var waveData = new float[K_sampleWindow];
        var micPosition = Microphone.GetPosition(null) - (K_sampleWindow + 1);

        if (micPosition < 0)
            return 0;

        _clipRecord.GetData(waveData, micPosition);

        return waveData.Take(K_sampleWindow).Max(data => data);
    }

}