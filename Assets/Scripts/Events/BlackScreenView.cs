using System;
using UniRx;
using UnityEngine;

namespace Events
{
    public class BlackScreenView : AbstractBasicTriggerView
    {
        private void Awake()
        {
            Screen.brightness = 0f;
            Camera.main.backgroundColor = Color.black;

            var serialDisposable = new SerialDisposable().AddTo(gameObject);
            var inputDetection = GameUtility.CreateInputDetection(InputDetection.ScreenBrightnessUp, serialDisposable, null);

            inputDetection.Triggered.Subscribe(_ => OnComplete()).AddTo(gameObject);
        }
    }
}
