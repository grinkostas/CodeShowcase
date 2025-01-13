using Game.Characters.Api;
using Game.Interact.Api;
using Game.Interact.Configs;
using UnityEngine;

namespace Game.Interact.Items
{
    public abstract class GrenadeItem : MonoBehaviour, IThrowItem, IRigidBodyUser
    {
        [SerializeField] private ThrowItemConfig _config;
        
        private Rigidbody _rigidbody;
        public Rigidbody rb => _rigidbody ??= GetComponent<Rigidbody>();

        private Collider _collider;
        public Collider col => _collider ??= GetComponent<Collider>();
        
        public InteractItemConfig config => _config;
        public GameObject itemGO => gameObject;

        public abstract void PrepareForThrow();

        public abstract void Throw();
        
    }
}