using Game.Interact.Api;
using Game.Interact.Configs;
using Core.Signals;
using UnityEngine;

namespace Game.Interact
{
    public abstract class PointerInteractItem : InteractItemBase, IPointerInteractItem, IPointerListener
    {
        [SerializeField] private InteractorType _interactorType;
        
        public override InteractItemConfig config { get; }
        
        private bool _pointerEntered = false;
        
        public Signal onPointerEnter { get; } = new();
        public Signal onPointerExit { get; } = new();

        public Signal<IInteractor> onPointerInteract { get; } = new();
        public Signal<IInteractor> onPointerStopInteract { get; } = new();

        public bool interacted { get; private set; } = false;

        public bool CanInteract(IInteractor interactor) => _interactorType.Compare(interactor);
        
        public void Interact(IInteractor interactor)
        {
            if (interacted)
            {
                interacted = false;
                OnEveryInteract(interactor);
                OnStopInteract(interactor);
                StopInteract();
                onPointerStopInteract.Dispatch(interactor);
                return;
            }
            interacted = true;
            OnEveryInteract(interactor);
            OnInteract(interactor);
            onStartInteract.Dispatch();
            onPointerInteract.Dispatch(interactor);
        }
        
        protected virtual void OnStopInteract(IInteractor interactor){}
        protected virtual void OnInteract(IInteractor interactor){}
        protected virtual void OnEveryInteract(IInteractor interactor){}
        
        public void OnPointerEnter()
        {
            if(_pointerEntered)
                return;
            _pointerEntered = true;
            onPointerEnter.Dispatch();
        }

        public void OnPointerExit()
        {
            if(_pointerEntered == false)
                return;
            _pointerEntered = false;
            onPointerExit.Dispatch();
        }

    }
}