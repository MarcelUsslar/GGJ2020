using System;
using System.Linq;
using UniRx;
using UnityEngine;

public class BatteryDetection : IInputDetection
{
    public IObservable<BatteryStatus> BatteryStatus { get; }

    public IObservable<Unit> Triggered { get; }

    public BatteryDetection(params BatteryStatus[] allowedBatteryStatuses)
    {
        BatteryStatus = Observable.EveryUpdate()
            .Select(_ => SystemInfo.batteryStatus)
            .DistinctUntilChanged();
        
        var isActive = BatteryStatus.Select(allowedBatteryStatuses.Contains);

        Triggered = isActive.Where(active => active).Select(_ => Unit.Default).Take(1);
    }
}