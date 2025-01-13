using System.Collections.Generic;
using NaughtyAttributes;
using Core.Extentions;
using UnityEngine;

namespace Core.Utilities
{
    public class Pulser : MonoBehaviour
    {
        [SerializeField] private bool _selfTarget;
        [SerializeField] private List<Transform> _targets;
        [SerializeField] private float _zoomDelta;
        [SerializeField] private float _zoomOutSpeed;
        [SerializeField] private float _maxScale;
        [SerializeField] private bool _customAxis;
        [SerializeField, ShowIf(nameof(_customAxis))] private Vector3 _scaleAxis;

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
        
        private float _currentScale = 1;
        private float scale
        {
            get => _currentScale;
            set
            {
                _currentScale = Mathf.Clamp(value, 1, _maxScale);
                SetScale(_currentScale);
            }
        }
        
        [Button]
        public void Punch()
        {
            scale += _zoomDelta;
        }
        
        private void Update()
        {
            scale -= _zoomOutSpeed * Time.deltaTime;
        }

        private void SetScale(float targetScale)
        {
            foreach (var target in transforms)
            {
                var scaleToSet = Vector3.one * targetScale;
                if (_customAxis) scaleToSet = _scaleAxis.NormalizeInvert() + _scaleAxis * targetScale;
                target.transform.localScale = scaleToSet;
            }
            
        }
    }
}