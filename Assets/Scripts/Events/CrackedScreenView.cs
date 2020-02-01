using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Events
{
    public class CrackedScreenView : AbstractBasicTriggerView
    {
        [SerializeField] private TrailRenderer _trail;
        [SerializeField] private Collider2D _trailHitBox;
        [SerializeField] private Transform _movedObject;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private List<GameObject> _triggers;

        private Camera _main;

        private void Awake()
        {
            _main = Camera.main;
            _main.backgroundColor = Color.white;
            _canvas.worldCamera = _main;

            _trail.enabled = false;
            Observable.EveryUpdate().Where(_ => Input.GetMouseButton(0)).Subscribe(_ => MoveMouse()).AddTo(gameObject);

            var onCollision = _trailHitBox.OnCollisionEnter2DAsObservable();
            onCollision.Subscribe(collision => _triggers.Remove(collision.gameObject)).AddTo(gameObject);
            onCollision.Where(_ => _triggers.Count == 0).Subscribe(_ => OnComplete()).AddTo(gameObject);
        }

        private void MoveMouse()
        {
            _trail.enabled = true;

            var mousePosition = _main.ScreenToWorldPoint(Input.mousePosition);
            _movedObject.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            _triggers.Remove(collision.gameObject);

            if (_triggers.Count == 0)
                OnComplete();
        }
    }
}
