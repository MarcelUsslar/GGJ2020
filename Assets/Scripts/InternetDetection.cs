using System;
using UniRx;
using UnityEngine;

public class InternetDetection : IInputDetection
{
    private readonly bool _allowMobileData;

    public IObservable<Unit> Triggered { get; }
    public IObservable<bool> Active { get; }

    public InternetDetection(bool allowMobileData, bool triggerWhenActive)
    {
        _allowMobileData = allowMobileData;
        var internetState = Observable.EveryUpdate()
            .Select(_ => Application.internetReachability)
            .DistinctUntilChanged();

        Active = internetState.Select(HasInternet);

        Triggered = Active
            .Where(active => active == triggerWhenActive)
            .Select(_ => Unit.Default).Take(1);
    }

    private bool HasInternet(NetworkReachability reachability)
    {
        return reachability == NetworkReachability.ReachableViaLocalAreaNetwork ||
               _allowMobileData && reachability == NetworkReachability.ReachableViaCarrierDataNetwork;
    }
}