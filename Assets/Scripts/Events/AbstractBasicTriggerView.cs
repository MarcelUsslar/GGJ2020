using System;
using UniRx;
using UnityEngine;

namespace Events
{
    public class AbstractBasicTriggerView : MonoBehaviour, ITriggerView
    {
        [SerializeField] private AudioSystem _audioSystem;
        protected AudioSystem AudioSystem => _audioSystem;

        public IObservable<Unit> Trigger => _trigger;

        private readonly Subject<Unit> _trigger = new Subject<Unit>();

        protected void OnComplete()
        {
            _audioSystem.OnSuccess();
            _trigger.OnNext(Unit.Default);
        }

        private void OnValidate()
        {
            if (_audioSystem != null)
                return;

            _audioSystem = GetComponent<AudioSystem>();
        }
    }
}