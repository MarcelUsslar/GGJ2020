using System;
using System.Linq;
using UniRx;
using UnityEngine;

public class BatteryDetection : IInputDetection
{
    public IObservable<BatteryStatus> BatteryStatus { get; }

    public IObservable<Unit> Triggered { get; }
    public IObservable<bool> Active { get; }

    public BatteryDetection(params BatteryStatus[] allowedBatteryStatuses)
    {
        BatteryStatus = Observable.EveryUpdate()
            .Select(_ => SystemInfo.batteryStatus)
            .DistinctUntilChanged();
        
        Active = BatteryStatus.Select(allowedBatteryStatuses.Contains);

        Triggered = Active.Where(active => active).Select(_ => Unit.Default).Take(1);
    }

    public void Dispose()
    { }
}