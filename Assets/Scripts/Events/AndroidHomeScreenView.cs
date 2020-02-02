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
            var buttonCount = new ReactiveProperty<int>(_buttons.Count).AddTo(gameObject);

            foreach (var button in _buttons)
            {
                button.OnDestroy.Subscribe(_ => _buttons.Remove(button)).AddTo(gameObject);
                button.OnDestroy.Where(_ => _buttons.Count == 0).Subscribe(_ => OnComplete()).AddTo(gameObject);

                button.OnClicked.Subscribe(_ => buttonCount.Value--).AddTo(gameObject);
                button.OnClicked.Where(unit => buttonCount.Value == 0).Subscribe(_ => UpdateButtonPosition(button)).AddTo(gameObject);
            }

            Observable.EveryUpdate()
                .Select(_ => buttonCount.Value == 0).DistinctUntilChanged()
                .Where(noButtons => noButtons).Take(1)
                .Subscribe(_ => ActivateDownloadButton())
                .AddTo(gameObject);
        }

        private void ActivateDownloadButton()
        {
            _downLoadIcon.SetActive(true);
        }

        private void UpdateButtonPosition(IconPress button)
        {
            DownLoadButtonPosition = button.transform.position;
            _downLoadIcon.transform.position = DownLoadButtonPosition;
        }
    }
}
