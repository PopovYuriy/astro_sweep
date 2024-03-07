using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Environment.InteractiveItems.GameActions.GameObjectActions
{
    public class GameObjectMoveAction : GameObjectActionBase
    {
        [SerializeField] private Vector3 _position;
        [SerializeField] private Vector3 _rotation;
        [SerializeField] private float _duration;

        private Tween _tween;

        private void OnDestroy()
        {
            KillTween();
        }

        public override void Execute(Action onComplete)
        {
            KillTween();

            _tween = DOTween.Sequence().Append(GameObject.transform.DOLocalMove(_position, _duration))
                .Append(GameObject.transform.DOLocalRotate(_rotation, _duration))
                .OnComplete(() => onComplete?.Invoke());
        }

        private void KillTween()
        {
            _tween?.Kill();
            _tween = null;
        }
    }
}