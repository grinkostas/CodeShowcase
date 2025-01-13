using Game.Characters.Api;
using Game.Interact.Api;
using UnityEngine;

namespace Game.Characters.Modules
{
    public class DownThrowModule : ThrowModule
    {
        [SerializeField] private ThrowItemsContainer _itemsContainer;
        [SerializeField] private float _moveVelocityMultiplier;
        [SerializeField] private float _throwVelocity;
        
        private IRigidBodyUser _rigidBodyUser;
        public IRigidBodyUser rigidBodyUser => _rigidBodyUser ??= GetComponentInParent<IRigidBodyUser>(true);
        
        public override Vector3 GetThrowDirection()
        {
            return rigidBodyUser.rb.transform.up * -1;
        }

        public override Vector3 GetThrowVelocity()
        {
            return rigidBodyUser.rb.velocity * _moveVelocityMultiplier + GetThrowDirection() * _throwVelocity;
        }

        public override IThrowItem GetThrowItem()
        {
            return _itemsContainer.GetThrowItem();
        }
    }
}