using System;
using UniRx;

namespace Events
{
    public interface ITriggerView
    {
        IObservable<Unit> Trigger { get; }
    }
}