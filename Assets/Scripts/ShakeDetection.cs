using System;
using UniRx;
using UnityEngine;

public class ShakeDetection : IInputDetection
{
    public IObservable<Unit> Triggered { get; }

    public ShakeDetection(float shakeThreshold)
    {
        var isActive = Observable.EveryUpdate()
            .Select(_ => Input.acceleration.magnitude > shakeThreshold);

        Triggered = isActive.Where(active => active).Select(_ => Unit.Default).Take(1);
    }
}
