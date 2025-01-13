using System.Collections.Generic;
using Game.Interact.Api;
using Game.Interact.Configs;
using Core.Signals;
using UnityEngine;

namespace Game.Interact
{
    [RequireComponent(typeof(Collider))]
    public class InteractItem : InteractItemBase
    {
        [SerializeField] private InteractItemConfig _itemConfig;

        public override InteractItemConfig config => _itemConfig;
        
        private List<IInteractor> _interactors = new();
        public IEnumerable<IInteractor> interactors => _interactors;
        
        public Signal<IInteractor> onEnter { get; } = new();
        public Signal<IInteractor> onExit { get; } = new();

        private IInteractCondition _condition;
        public IInteractCondition condition => _condition ??= GetComponent<IInteractCondition>();

        private void OnCollisionEnter(Collision other)=> TryEnter(other.gameObject);
        private void OnTriggerEnter(Collider other) => TryEnter(other.gameObject);       
        private void TryEnter(GameObject target)
        {
            if (target.TryGetComponent(out IInteractor interactor) == false)
                return;
            if (condition != null && condition.CanInteract(interactor) == false)
                return;
            Enter(interactor);
        }

        private void Enter(IInteractor interactor)
        {
            _interactors.Add(interactor);
            OnEnter(interactor);
            onEnter.Dispatch(interactor);
        }
        protected virtual void OnEnter(IInteractor interactor){}

        private void OnCollisionExit(Collision other) => TryExit(other.gameObject);
        private void OnTriggerExit(Collider other) => TryExit(other.gameObject);
        private void TryExit(GameObject target)
        {
            if (target.TryGetComponent(out IInteractor interactor) == false)
                return;
            if(_interactors.Contains(interactor) == false)
                return;
            Exit(interactor);
        }

        private void Exit(IInteractor interactor)
        {
            _interactors.Remove(interactor);
            OnEnter(interactor);
            onExit.Dispatch(interactor);
        }
        protected virtual void OnExit(IInteractor interactor){}
    }
}