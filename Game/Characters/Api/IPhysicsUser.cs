using UnityEngine;

namespace Game.Characters.Api
{
    public interface IRigidBodyUser
    {
        public Rigidbody rb { get; }
        public Collider col { get; }
    }
}