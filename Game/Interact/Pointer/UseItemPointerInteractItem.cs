using Game.Characters;
using Game.Interact.Api;
using Game.Interact.Configs;
using Core.Signals;
using UnityEngine;

namespace Game.Interact
{
    public class UseItemPointerInteractItem : PointerInteractItem
    {
        [SerializeField] private bool _oneUse;
        [SerializeField] private PickUpItemConfig _interactConfig;

        private bool _used = false;
        
        public Signal<IPickUpItem> onUseItem { get; } = new();
        
        protected override void OnInteract(IInteractor interactor)
        {
            if(_used && _oneUse)
                return;
            if(interactor is not Player)
                return;
            var pickUpModule = ((Player)interactor).pickUpItemModule;
            if(pickUpModule.pickedItem == null)
                return;
            if(pickUpModule.pickedItem.config.id != _interactConfig.id)
                return;
            var item = pickUpModule.Take();
            _used = true;
            OnUseItem(item);
            onUseItem.Dispatch(item);
        }
        
        protected virtual void OnUseItem(IPickUpItem item){}
    }
}