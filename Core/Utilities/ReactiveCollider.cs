using System.Collections.Generic;
using Core.Signals;
using UnityEngine;

namespace Core.Utilities.Enums
{
    public class ReactiveCollider : MonoBehaviour
    {
        public Signal<Collider> onTriggerEnter { get; } = new();
        public Signal<Collider> onTriggerExit { get; } = new();

        private List<Collider> _colliders = new();
        public IEnumerable<Collider> colliders => _colliders;
        
        private void OnTriggerEnter(Collider other)
        {
            _colliders.Add(other);
            onTriggerEnter.Dispatch(other);
        }
        
        private void OnTriggerExit(Collider other)
        {
            _colliders.Remove(other);
            onTriggerExit.Dispatch(other);
        }
    }
}