using System;
using System.Linq;
using UniRx;
using UnityEngine;

public class InternetDetection : IInputDetection
{
    public IObservable<Unit> Triggered { get; }
    public IObservable<bool> Active { get; }

    public InternetDetection(params NetworkReachability[] allowedNetworkStates)
    {
        var internetState = Observable.EveryUpdate()
            .Select(_ => Application.internetReachability)
            .DistinctUntilChanged();

        Active = internetState.Select(allowedNetworkStates.Contains);

        Triggered = Active.Select(_ => Unit.Default).Take(1);
    }
}