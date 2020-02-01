using System;
using UniRx;

public interface IInputDetection
{
    IObservable<Unit> Triggered { get; }
}