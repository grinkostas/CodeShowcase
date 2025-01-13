using Game.Characters.Api;
using Core.Utilities;
using UnityEngine;

namespace Game.Characters.Core
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Character : InjectableMono, IRigidBodyUser
    {
        [SerializeField] private Transform _modelParent;
        
        private Rigidbody _rigidbody;
        public Rigidbody rb
        {
            get
            {
                if (_rigidbody != null) return _rigidbody;
                _rigidbody = GetComponent<Rigidbody>();
                return _rigidbody;
            }
        }
        
        private Collider _collider;
        public Collider col
        {
            get
            {
                if (_collider != null) return _collider;
                _collider = GetComponent<Collider>();
                return _collider;
            }
        }
        public Transform modelParent => _modelParent;
    }
}