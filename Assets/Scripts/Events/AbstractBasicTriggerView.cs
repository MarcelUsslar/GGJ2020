using System;
using UniRx;
using UnityEngine;

namespace Events
{
    public class AbstractBasicTriggerView : MonoBehaviour, ITriggerView
    {
        public IObservable<Unit> Trigger => _trigger;

        private readonly Subject<Unit> _trigger = new Subject<Unit>();

        protected void OnComplete()
        {
            _trigger.OnNext(Unit.Default);
        }
    }
}