using UnityEngine;

namespace Game.Characters.AI.Data
{
    public struct AIMoveResult
    {
        public AICharacter character;
        public Vector3 destination;
        public MoveResult moveResult;

        public AIMoveResult(AICharacter character, Vector3 destination, MoveResult result = MoveResult.Started)
        {
            this.character = character;
            this.destination = destination;
            moveResult = result;
        }
    }
}