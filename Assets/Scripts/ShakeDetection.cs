using System;
using UniRx;
using UnityEngine;

public class ShakeDetection : IInputDetection
{
    private readonly float _shakeThreshold;

    public IObservable<Unit> Triggered { get; }
    public IObservable<bool> Active { get; }

    public ShakeDetection(float shakeThreshold, bool triggerWhenActive)
    {
        _shakeThreshold = shakeThreshold;

        Active = Observable.EveryUpdate()
            .Select(_ => Input.acceleration.magnitude > _shakeThreshold);

        Triggered = Active
            .Where(active => active == triggerWhenActive)
            .Select(_ => Unit.Default).Take(1);
    }
}
