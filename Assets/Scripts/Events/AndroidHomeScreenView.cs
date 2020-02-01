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
            foreach (var button in _buttons)
            {
                button.OnDestroy.Subscribe(_ => _buttons.Remove(button)).AddTo(gameObject);
                button.OnDestroy.Where(_ => _buttons.Count == 0).Subscribe(_ => OnComplete()).AddTo(gameObject);
            }

            Observable.EveryUpdate()
                .Select(_ => _buttons.Count(button => button.IsClicked))
                .DistinctUntilChanged().Where(buttons => buttons == 1 && _buttons.Count == 1).Take(1)
                .Subscribe(_ => ActivateDownloadButton())
                .AddTo(gameObject);
        }

        private void ActivateDownloadButton()
        {
            _downLoadIcon.SetActive(true);
            DownLoadButtonPosition = _buttons[0].transform.position;
            _downLoadIcon.transform.position = DownLoadButtonPosition;
        }
    }
}
