using Game.Characters.AI.Notifications.Notifiers;
using Game.Characters.Api;
using Game.Characters.Core;
using Game.Characters.Modules;
using Game.Characters.Utilities;
using Game.Interact.Api;
using UnityEngine;

namespace Game.Characters
{
    public class Player : Character, IPhysicsInteractor, INotifyTarget, IAnimatorUser
    {
        [SerializeField] private SprintModule sprintModule;
        [SerializeField] private StaminaModule _staminaModule;
        [SerializeField] private PickUpItemModule _pickUpItemModule;
        [SerializeField] private AnimatorLinker _animatorLinker;
        
        public SprintModule sprint => sprintModule;
        public PickUpItemModule pickUpItemModule => _pickUpItemModule;
        public StaminaModule stamina => _staminaModule;
        public float weight => 10;
        public Animator animator => _animatorLinker.animator;
    }
}