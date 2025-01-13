using NaughtyAttributes;
using UnityEngine;

namespace Core.Utilities
{
    public class SimpleRotator : MonoBehaviour
    {
        [SerializeField] private bool _selfTarget;
        [SerializeField, HideIf(nameof(_selfTarget))] private Transform _target;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private Vector3 _rotateAxis;
        [SerializeField] private bool _unscaledTime;
        
        private Transform target => _selfTarget ? transform : _target;
        
        private void Update()
        {
            var time = Time.deltaTime;
            if (_unscaledTime)
                time = Time.unscaledDeltaTime;
            target.rotation *= Quaternion.Euler(_rotateAxis * (_rotateSpeed * time));
        }
    }
}