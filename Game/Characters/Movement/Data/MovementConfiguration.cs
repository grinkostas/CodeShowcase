using UnityEngine;

namespace Game.Characters.Movement.Data
{
    [CreateAssetMenu(menuName = "Game/Movement Configuration")]
    public class MovementConfiguration : ScriptableObject
    {
        public float movementSpeed = 4f;
        public float airControlRate = 2f;
        public float jumpSpeed = 7f;
        public float jumpDuration = 0.2f;
        public float airFriction = 0.5f;
        public float groundFriction = 100f;
        public float gravity = 30f;
        [Tooltip("How fast the character will slide down steep slopes.")]
        public float slideGravity = 5f;
        public float slopeLimit = 80f;
        [Tooltip("Whether to calculate and apply momentum relative to the controller's transform.")]
        public bool useLocalMomentum = false;
    }
}