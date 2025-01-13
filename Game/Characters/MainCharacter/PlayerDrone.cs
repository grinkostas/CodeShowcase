using Game.Characters.Api;
using Game.Characters.Core;
using Game.Characters.Modules;
using Game.Interact;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Characters
{
    public class PlayerDrone : Character, IWirelessUser
    {
        [SerializeField] private WirelessModule _wirelessModule;
        public WirelessModule wirelessModule => _wirelessModule;
        
        [Inject, UsedImplicitly] public InteractModule interactModule { get; }

        protected override void OnInjectComplete()
        {
            if(gameObject.activeInHierarchy)
                interactModule.SetInteractor(this);
        }
    }
}