using Game.Interact.Api;
using UnityEngine;

namespace Game.Interact.Physical
{
    public class PhysicalInteractor : MonoBehaviour, IPhysicsInteractor
    {
        [SerializeField] private float _weight;
        public float weight => _weight;
    }
}