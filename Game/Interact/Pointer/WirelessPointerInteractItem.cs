using Game.Characters.Api;
using Game.Interact.Api;
using Core.Signals;
using UnityEngine;

namespace Game.Interact
{
    public class WirelessPointerInteractItem : PointerInteractItem, IWirelessItem, IInteractActiveChanger
    {
        [SerializeField] private bool _turnedOn;

        private bool _isTurnOn;

        public Signal onTurnOn { get; } = new();
        public Signal onTurnOff { get; } = new();
        
        public Signal<bool> onChangeActiveStatus { get; } = new();

        private void Awake()
        {
            _isTurnOn = _turnedOn;
            if(_isTurnOn) 
                TurnOn();
            else 
                TurnOff();
        }

        protected override void OnEveryInteract(IInteractor interactor)
        {
            if(interactor is IWirelessUser == false)
                return;
            if(_isTurnOn) 
                TurnOff();
            else 
                TurnOn();
        }

        public void TurnOff()
        {
            _isTurnOn = false;
            OnTurnOff();
            onTurnOff.Dispatch();
            onChangeActiveStatus.Dispatch(false);
        }
        protected virtual void OnTurnOff(){}

        public void TurnOn()
        {
            _isTurnOn = true;
            OnTurnOn();
            onTurnOn.Dispatch();
            onChangeActiveStatus.Dispatch(true);
        }
        protected virtual void OnTurnOn(){}
    }
}