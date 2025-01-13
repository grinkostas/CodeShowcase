using System;
using Game.Characters.Modules;
using UnityEngine;

namespace Game.Characters.Stamina
{
    public class SprintStaminaUser : PlayerModule
    {
        [SerializeField] private float _staminaCost;
        
        private SprintModule _sprintModule;
        public SprintModule sprintModule => _sprintModule ??= GetComponent<SprintModule>();

        private void Update()
        {
            if(sprintModule.isSprinting == false)
                return;
            
            if (player.stamina.TryUse(_staminaCost * Time.deltaTime) == false)
                sprintModule.StopSprint();
        }
    }
}