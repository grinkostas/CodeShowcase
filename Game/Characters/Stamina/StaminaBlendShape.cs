using System;
using Game.Characters.Modules;
using UnityEngine;

namespace Game.Characters.Stamina
{
    public class StaminaBlendShape : PlayerModule
    {
        [SerializeField] private float _maxStaminaBlendAmount;
        [SerializeField] private bool _inverted;
        
        [SerializeField] private SkinnedMeshRenderer _skinnedMesh;

        private void OnEnable()
        {
            if(_skinnedMesh == null)
                return;
            player.stamina.onStaminaChange.On(OnStaminaChange);
        }

        private void OnDisable()
        {
            player.stamina.onStaminaChange.Off(OnStaminaChange);
        }

        private void OnStaminaChange()
        {
            Debug.Log(GetBlendAmount());
            _skinnedMesh.SetBlendShapeWeight(0, GetBlendAmount());
        }

        private float GetBlendAmount()
        {
            if (_inverted == false)
                return _maxStaminaBlendAmount * player.stamina.staminaRelative;
            return _maxStaminaBlendAmount - _maxStaminaBlendAmount * player.stamina.staminaRelative;
        }
    }
}