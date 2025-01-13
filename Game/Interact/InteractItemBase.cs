using Game.Interact.Api;
using Game.Interact.Configs;
using Core.Signals;
using UnityEngine;

namespace Game.Interact
{
    public abstract class InteractItemBase : MonoBehaviour, IInteractItem
    {
        public Signal onStartInteract { get; } = new();
        public Signal onStopInteract { get; } = new();
        
        public abstract InteractItemConfig config { get; }
        public GameObject itemGO => gameObject;
        
        public void Interact()
        {
            onStartInteract.Dispatch();
        }

        public void StopInteract()
        {
            onStopInteract.Dispatch();
        }
    }
}