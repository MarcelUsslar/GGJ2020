using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Events
{
    public class EnglishSettingsMenuView : AbstractBasicTriggerView
    {
        [SerializeField] private RectTransform _error1;
        [SerializeField] private RectTransform _error2;
        [SerializeField] private RectTransform _error3;
        [SerializeField] private RectTransform _errorText;
        
        private bool _animate;

        private readonly Dictionary<RectTransform, Vector2> _startMinAnchor = new Dictionary<RectTransform, Vector2>();
        private readonly Dictionary<RectTransform, Vector2> _startMaxAnchor = new Dictionary<RectTransform, Vector2>();

        private void Awake()
        {
            var serialDisposable = new SerialDisposable().AddTo(gameObject);
            var inputDetection = GameUtility.CreateInputDetection(InputDetection.Battery, serialDisposable, null);

            inputDetection.Triggered.Subscribe(_ => _animate = true).AddTo(gameObject);
            inputDetection.Triggered.Delay(TimeSpan.FromSeconds(1)).Subscribe(_ => OnComplete()).AddTo(gameObject);

            Observable.EveryUpdate().Where(_ => _animate).Subscribe(_ => Vacuum()).AddTo(gameObject);

            InitializeVacuum();
        }

        private void InitializeVacuum()
        {
            InitializeVacuum(_error1);
            InitializeVacuum(_error2);
            InitializeVacuum(_error3);
            InitializeVacuum(_errorText);
        }

        private void InitializeVacuum(RectTransform rectTransform)
        {
            _startMinAnchor.Add(rectTransform, rectTransform.anchorMin);
            _startMaxAnchor.Add(rectTransform, rectTransform.anchorMax);
        }

        private void Vacuum()
        {
            Vacuum(_error1);

            Vacuum(_error2);

            Vacuum(_error3);

            Vacuum(_errorText);
        }

        private void Vacuum(RectTransform objectTransform)
        {
            var startMin = _startMinAnchor[objectTransform];
            var startMax = _startMaxAnchor[objectTransform];
            objectTransform.anchorMin = new Vector2(startMin.x, Mathf.Lerp(objectTransform.anchorMin.y, startMin.y - 1, 0.075f));
            objectTransform.anchorMax = new Vector2(startMax.x, Mathf.Lerp(objectTransform.anchorMax.y, startMax.y - 1, 0.075f));

            objectTransform.localScale = new Vector3(Mathf.Lerp(objectTransform.localScale.x, 0.05f, 0.1f), 1, 1);
        }
    }
}
