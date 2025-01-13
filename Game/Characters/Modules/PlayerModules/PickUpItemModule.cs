using DG.Tweening;
using Game.Interact;
using Game.Interact.Api;
using Core.Signals;
using UnityEngine;

namespace Game.Characters.Modules
{
    public class PickUpItemModule : MonoBehaviour
    {
        [SerializeField] private Transform _followPoint;
        [SerializeField] private FollowingItem _followingContainer;

        
        public IPickUpItem pickedItem { get; private set; }

        public Signal<IPickUpItem> onPickUp { get; } = new();
        public Signal<IPickUpItem> onGetOut { get; } = new();
        
        private void Awake()
        {
            _followingContainer.transform.SetParent(null);
        }

        public void TryPickUpItem(IPickUpItem item)
        {
            ThrowCurrentItem();
            PickUpItem(item);
        }
        
        private void PickUpItem(IPickUpItem item)
        {
            pickedItem = item;
            _followingContainer.transform.position = pickedItem.itemGO.transform.position;
            pickedItem.itemGO.transform.SetParent(_followingContainer.transform);
            pickedItem.PickUp();
            DOVirtual.DelayedCall(0.5f, () =>
            {
                _followingContainer.StartFollow(_followPoint);
            });
            onPickUp.Dispatch(item);
        }
        
        public void ThrowCurrentItem()
        {
            if (pickedItem == null) 
                return;
            ThrowItem(pickedItem);
        }
        
        public void ThrowItem(IPickUpItem item)
        {
            if (item != pickedItem) 
                return;
            Take();
        }

        public IPickUpItem Take()
        {
            if (pickedItem == null) 
                return null;
            
            var item = pickedItem;
            _followingContainer.EndFollow();
            item.itemGO.transform.SetParent(null);
            onGetOut.Dispatch(item);
            pickedItem = null;
            return item;
        }
    }
}