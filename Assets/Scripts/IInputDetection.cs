using System;
using UniRx;

public interface IInputDetection : IDisposable
{
    IObservable<Unit> Triggered { get; }
    IObservable<bool> Active { get; }
}