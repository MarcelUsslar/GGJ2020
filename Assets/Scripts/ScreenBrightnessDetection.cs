using System;
using UniRx;
using UnityEngine;

public class ScreenBrightnessDetection : IInputDetection
{
    public IObservable<float> ScreenBrightness { get; }

    public IObservable<Unit> Triggered { get; }

    public ScreenBrightnessDetection(Func<float, bool> hasReachedBrightness)
    {
        ScreenBrightness = Observable.EveryUpdate()
            .Select(_ => Screen.brightness)
            .DistinctUntilChanged();

        Triggered = ScreenBrightness.Select(hasReachedBrightness).Where(active => active)
            .Select(_ => Unit.Default).Take(1);
    }
}