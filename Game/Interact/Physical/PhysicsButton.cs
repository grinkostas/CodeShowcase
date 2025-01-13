using System;
using Game.Interact.Api;
using Game.Interact.Conditions;
using UnityEngine;

namespace Game.Interact.Physical
{
    [RequireComponent(typeof(PhysicsInteractCondition))]
    public class PhysicsButton : InteractItemListener
    {
        [SerializeField] private float _targetWeight;

        public bool pressed { get; private set; } = false;
        
        private float _enteredWeight = 0;
        public float enteredWeight
        {
            get => _enteredWeight;
            private set => _enteredWeight = Math.Max(0, value);
        }

        protected override void OnEnter(IInteractor interactor)
        {
            enteredWeight += ((IPhysicsInteractor)interactor).weight;
            if (enteredWeight >= _targetWeight)
                Press();
        }

        protected override void OnExit(IInteractor interactor)
        {
            enteredWeight -= ((IPhysicsInteractor)interactor).weight;
            if(enteredWeight < _targetWeight)
                UnPress();
        }

        private void Press()
        {
            if(pressed)
                return;
            pressed = true;
            interactItem.Interact();
        }

        private void UnPress()
        {
            if(pressed == false)
                return;
            pressed = false;
            interactItem.StopInteract();
        }
    }
}