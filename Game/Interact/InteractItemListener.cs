using Game.Interact.Api;
using UnityEngine;

namespace Game.Interact
{
    [RequireComponent(typeof(InteractItem))]
    public abstract class InteractItemListener : MonoBehaviour
    {
        private InteractItem _interactItem;
        public InteractItem interactItem => _interactItem ??= GetComponent<InteractItem>();

        private void OnEnable()
        {
            interactItem.onEnter.On(OnEnter);
            interactItem.onExit.On(OnExit);
        }

        private void OnDisable()
        {
            interactItem.onEnter.Off(OnEnter);
            interactItem.onExit.Off(OnExit);
        }

        protected abstract void OnEnter(IInteractor interactor);
        protected abstract void OnExit(IInteractor interactor);
    }
}