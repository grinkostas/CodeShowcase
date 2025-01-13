using DG.Tweening;
using Game.Interact.Api;
using UnityEngine;

namespace Game.Characters.Modules
{
    public abstract class ThrowModule : MonoBehaviour
    {
        public abstract Vector3 GetThrowDirection();
        public abstract Vector3 GetThrowVelocity();
        public abstract IThrowItem GetThrowItem();
        
        public void Throw()
        {
            var item = GetThrowItem();
            item.itemGO.transform.SetParent(null);
            item.PrepareForThrow();
            item.rb.AddForce(GetThrowVelocity(), ForceMode.VelocityChange);
            item.Throw();
            item.itemGO.transform.DOLookAt(GetThrowDirection(), 0.25f);
        }
    }
}