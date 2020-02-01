using System;
using UniRx;
using UnityEngine;

public class ShakeDetection : IInputDetection
{
    public IObservable<Unit> Triggered { get; }
    public IObservable<bool> Active { get; }

    public ShakeDetection(float shakeThreshold, bool triggerWhenActive)
    {
        Active = Observable.EveryUpdate()
            .Select(_ => Input.acceleration.magnitude > shakeThreshold);

        Triggered = Active
            .Where(active => active == triggerWhenActive)
            .Select(_ => Unit.Default).Take(1);
    }

    public void Dispose()
    { }
}
