using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Events
{
    public class AndroidHomeScreenDownloadButton : AbstractBasicTriggerView
    {
        [SerializeField] private Button _downloadButton;
        [SerializeField] private GameObject _downloadingIcon;

        private void Awake()
        {
            _downloadButton.transform.position = AndroidHomeScreenView.DownLoadButtonPosition;

            var serialDisposable = new SerialDisposable().AddTo(gameObject);
            var inputDetection = GameUtility.CreateInputDetection(InputDetection.Internet, serialDisposable, null) as InternetDetection;

            var inAllowedState = inputDetection.InAllowedState.ToReadOnlyReactiveProperty().AddTo(gameObject);
            var onAllowedClick = _downloadButton.OnClickAsObservable().Where(_ => inAllowedState.Value);

            onAllowedClick.Subscribe(_ => ShowDownloadingIcon()).AddTo(gameObject);
            onAllowedClick.Delay(TimeSpan.FromSeconds(5)).Subscribe(_ => OnComplete()).AddTo(gameObject);

            Observable.EveryUpdate().Subscribe(_ => _downloadingIcon.transform.Rotate(new Vector3(0, 0, -5f))).AddTo(gameObject);
        }

        private void ShowDownloadingIcon()
        {
            _downloadingIcon.SetActive(true);
            _downloadingIcon.transform.position = _downloadButton.transform.position;

            _downloadButton.gameObject.SetActive(false);
        }
    }
}
