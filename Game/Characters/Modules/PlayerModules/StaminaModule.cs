using System;
using Game.Control;
using Core.Signals;
using UnityEngine;

namespace Game.Characters.Modules
{
    public class StaminaModule : MonoBehaviour
    {
        [SerializeField] private float _startStamina;

        private bool _ended = false;
        
        //For Debug
        [SerializeField] private float _currentStamina;
        public float currentStamina
        {
            get => _currentStamina;
            set
            {
                _currentStamina = Mathf.Clamp(value, 0, _startStamina);
                if (value <= 0.01f)
                {
                    _currentStamina = 0;
                    if(_ended == false)
                        onStaminaEnd.Dispatch();
                    _ended = true;
                }
                else
                {
                    _ended = false;
                }
                onStaminaChange.Dispatch();
            }
        }

        public float maxStamina => _startStamina;
        public float staminaRelative => currentStamina / maxStamina;
        
        public Signal onStaminaChange { get; } = new();
        public Signal onStaminaEnd { get; } = new();

        private void Awake()
        {
            _currentStamina = _startStamina;
        }


        public bool TryUse(float amount)
        {
            if (CanUse(amount) == false)
                return false;
            Use(amount);
            return true;
        }
        
        public bool CanUse(float amount)
        {
            return currentStamina >= amount;
        }
        
        private void Use(float amount)
        {
            currentStamina -= amount;
        }

    }
}