using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Events
{
    public class BlueScreenView : AbstractBasicTriggerView
    {
        [SerializeField] private Button _screenButton;

        private void Awake()
        {
            Observable.Timer(TimeSpan.FromSeconds(1))
                .SelectMany(_ => _screenButton.OnClickAsObservable())
                .Subscribe(_ => OnComplete()).AddTo(gameObject);
            ;
        }
    }
}
