using System;
using System.Linq;
using UniRx;
using UnityEngine;

public class InternetDetection : IInputDetection
{
    public IObservable<NetworkReachability> InternetState { get; }

    public IObservable<bool> InAllowedState { get; }

    public IObservable<Unit> Triggered { get; }

    public InternetDetection(params NetworkReachability[] allowedNetworkStates)
    {
        InternetState = Observable.EveryUpdate()
            .Select(_ => Application.internetReachability)
            .DistinctUntilChanged();

        InAllowedState = InternetState.Select(allowedNetworkStates.Contains);

        Triggered = InAllowedState.Where(active => active).Select(_ => Unit.Default);
    }
}