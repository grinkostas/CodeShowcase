using Game.Interact.Api;
using UnityEngine;

namespace Game.Interact.Conditions
{
    public class PhysicsInteractCondition : MonoBehaviour, IInteractCondition
    {
        public bool CanInteract(IInteractor interactor)
        {
            return interactor is IPhysicsInteractor;
        }
    }
}