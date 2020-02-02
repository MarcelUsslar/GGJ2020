using System;
using UniRx;
using UnityEngine;

namespace Events
{
    public class EnglishSettingsMenuView : AbstractBasicTriggerView
    {
        [SerializeField] private GameObject _error1;
        [SerializeField] private GameObject _error2;
        [SerializeField] private GameObject _error3;
        [SerializeField] private GameObject _errorText;

        private void Awake()
        {
            var serialDisposable = new SerialDisposable().AddTo(gameObject);
            var inputDetection = GameUtility.CreateInputDetection(InputDetection.Battery, serialDisposable, null);

            var animate = false;
            inputDetection.Triggered.Subscribe(_ => animate = true).AddTo(gameObject);
            inputDetection.Triggered.Delay(TimeSpan.FromSeconds(1)).Subscribe(_ => OnComplete()).AddTo(gameObject);

            Observable.EveryUpdate().Where(_ => animate).Subscribe(_ => Vacuum()).AddTo(gameObject);
        }

        private void Vacuum()
        {
            var error1Tran = _error1.transform;

            _error1.transform.position = new Vector3(0, Mathf.Lerp(error1Tran.position.y,-10f,0.05f), 1);
            _error1.transform.localScale = new Vector3 (Mathf.Lerp(error1Tran.localScale.x, 0.05f, 0.1f), Mathf.Lerp(error1Tran.localScale.y, 1f, 0.5f), 1);

            var error2Tran = _error2.transform;

            _error2.transform.position = new Vector3(0, Mathf.Lerp(error2Tran.position.y, -10f, 0.05f), 1);
            _error2.transform.localScale = new Vector3(Mathf.Lerp(error2Tran.localScale.x, 0.05f, 0.1f), Mathf.Lerp(error2Tran.localScale.y, 1f, 0.5f), 1);

            var error3Tran = _error3.transform;

            _error3.transform.position = new Vector3(0, Mathf.Lerp(error3Tran.position.y, -10f, 0.05f), 1);
            _error3.transform.localScale = new Vector3(Mathf.Lerp(error3Tran.localScale.x, 0.05f, 0.1f), Mathf.Lerp(error3Tran.localScale.y, 1f, 0.5f), 1);

            var errorTextTran = _errorText.transform;

            _errorText.transform.position = new Vector3(0, Mathf.Lerp(errorTextTran.position.y, -10f, 0.04f), 1);
            _errorText.transform.localScale = new Vector3(Mathf.Lerp(errorTextTran.localScale.x, 0.04f, 0.1f), Mathf.Lerp(errorTextTran.localScale.y, 1f, 0.4f), 1);
        }
    }
}
