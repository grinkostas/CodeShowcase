using GameCore.Core.Scripts.TransformUtilities;
using UnityEngine;

namespace Game.Interact.Items
{
    public class CameraInteractItem : WirelessPointerInteractItem
    {
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private LoopRotator _engine;

        protected override void OnTurnOn()
        {
            _particle.Play();
            _engine.PrepareAndStartLoop();
        }
        
        protected override void OnTurnOff()
        {
            _particle.Stop();
            _engine.StopLoop();
        }

    }
}