using System;
using System.Linq;
using UniRx;
using UnityEngine;

public class BatteryDetection : IInputDetection
{
    public IObservable<Unit> Triggered { get; }
    public IObservable<bool> Active { get; }

    public BatteryDetection(params BatteryStatus[] allowedBatteryStatuses)
    {
        var batteryStatus = Observable.EveryUpdate()
            .Select(_ => SystemInfo.batteryStatus)
            .DistinctUntilChanged();
        
        Active = batteryStatus.Select(allowedBatteryStatuses.Contains);

        Triggered = Active.Select(_ => Unit.Default).Take(1);
    }
}