using CMF;
using DG.Tweening;
using Game.Control;
using Core.Signals;
using UnityEngine;

namespace Game.Characters.Modules
{
    public class SprintModule : PlayerModule, IHotkeyUser
    {
        [SerializeField] private float _speedBonus;
        [SerializeField] private float _acceleratioDuration;
        
        private Mover _mover;
        public Mover mover => _mover ??= GetComponentInParent<Mover>(true);
        
        private AdvancedWalkerController _walkerController;
        public AdvancedWalkerController walkerController => _walkerController ??= GetComponentInParent<AdvancedWalkerController>(true);

        private float _startSpeed;
        
        private Tween _sprintTween;
        private Tween _brakeTween;

        public bool isSprinting { get; private set; } = false;

        public Signal onStartSprint { get; } = new();
        public Signal onStopSprint { get; } = new();
        
        private void Awake()
        {
            _startSpeed = walkerController.movementSpeed;
        }
        
        public void OnPress() => StartSprint();
        public void StartSprint()
        {
            if(isSprinting)
                return;
            isSprinting = true;
            _brakeTween?.Kill();
            _sprintTween = DOVirtual.Float(walkerController.movementSpeed, _startSpeed + _speedBonus, _acceleratioDuration, SetSpeed)
                .OnComplete(()=>_sprintTween = null)
                .OnKill(()=>_sprintTween = null)
                .SetUpdate(false);
            onStartSprint.Dispatch();
        }
        
        public void OnRepel() => StopSprint();
        public void StopSprint()
        {
            if(isSprinting == false)
                return;
            if(_brakeTween != null)
                return;
            _sprintTween?.Kill();
            _brakeTween = DOVirtual.Float(walkerController.movementSpeed, _startSpeed, _acceleratioDuration, SetSpeed)
                .OnComplete(()=>
                {
                    _brakeTween = null;
                    isSprinting = false;
                })
                .OnKill(()=>_brakeTween = null)
                .SetUpdate(false);
            onStopSprint.Dispatch();
        }

        private void SetSpeed(float speed)
        {
            walkerController.movementSpeed = speed;
        }
    }
}