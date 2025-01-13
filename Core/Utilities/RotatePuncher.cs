using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Utilities
{
    public class RotatePuncher : MonoBehaviour
    {
        [SerializeField] private bool _selfTarget;
        [SerializeField] private List<Transform> _targets;
        [SerializeField] private float _rotateDelta;
        [SerializeField] private Vector3 _rotateAxis;
        [SerializeField] private float _rotateDuration;

        private List<Transform> _transforms;
        public List<Transform> transforms
        {
            get
            {
                if (_transforms != null) return _transforms;
                
                _transforms = new(_targets);
                if(_selfTarget)
                    _transforms.Add(transform);
                return _transforms;
            }
        }
        
        private float _currentRotation = 0;
        private float rotation
        {
            get => _currentRotation;
            set
            {
                _currentRotation = Mathf.Clamp(value, -_rotateDelta, _rotateDelta);
                SetRotate(_currentRotation);
            }
        }

        private Sequence currentSequence;

        [Button]
        public void Punch()
        {
            if (rotation < 0)
                rotation = _rotateDelta;
            else
                rotation = -_rotateDelta;
        }
        
        private void SetRotate(float targetScale)
        {
            currentSequence ??= DOTween.Sequence();
            currentSequence.Append(Rotate(targetScale));
            currentSequence.onComplete ??= () => Rotate(0f);
        }

        private Tween Rotate(float endValue)
        {
            var sequence = DOTween.Sequence();
            foreach (var target in transforms)
                sequence.Join(target.transform.DOLocalRotate(_rotateAxis * rotation, _rotateDuration));
            return sequence;
        }
    }
}