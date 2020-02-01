using System;
using System.Linq;
using UniRx;
using UnityEngine;

public class InternetDetection : IInputDetection
{
    public IObservable<NetworkReachability> InternetState { get; }

    public IObservable<Unit> Triggered { get; }
    public IObservable<bool> Active { get; }

    public InternetDetection(params NetworkReachability[] allowedNetworkStates)
    {
        InternetState = Observable.EveryUpdate()
            .Select(_ => Application.internetReachability)
            .DistinctUntilChanged();

        Active = InternetState.Select(allowedNetworkStates.Contains);

        Triggered = Active.Where(active => active).Select(_ => Unit.Default).Take(1);
    }

    public void Dispose()
    { }
}