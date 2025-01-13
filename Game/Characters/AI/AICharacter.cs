using Game.Characters.Core;
using UnityEngine;

namespace Game.Characters.AI
{
    public class AICharacter : Character
    {
        [SerializeField] private AIMovement _movement;
        public AIMovement movement => _movement;
    }
}