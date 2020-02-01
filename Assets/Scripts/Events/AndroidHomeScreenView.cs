using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Events
{
    public class AndroidHomeScreenView : AbstractBasicTriggerView
    {
        public static Vector3 DownLoadButtonPosition;

        [SerializeField] private List<IconPress> _buttons;
        [SerializeField] private GameObject _downLoadIcon;
        
        private void Awake()
        {
            var availableButtons = Observable.EveryUpdate().Select(_ => _buttons.Count(button => button != null));

            availableButtons.Where(buttons => buttons == 0)
                .Subscribe(_ => OnComplete()).AddTo(gameObject);

            availableButtons.Where(buttons => buttons == 1)
                .Select(_ => _buttons.Single(button => button.IsClicked)).Take(1)
                .Subscribe(ActivateDownloadButton).AddTo(gameObject);
        }

        private void ActivateDownloadButton(IconPress button)
        {
            _downLoadIcon.SetActive(true);
            DownLoadButtonPosition = button.transform.position;
            _downLoadIcon.transform.position = DownLoadButtonPosition;
        }
    }
}
