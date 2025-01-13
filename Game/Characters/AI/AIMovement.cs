using Game.Characters.AI.Data;
using NaughtyAttributes;
using ProjectDawn.Navigation.Hybrid;
using Core.Signals;
using UnityEngine;

namespace Game.Characters.AI
{
    public class AIMovement : MonoBehaviour
    {
        private AICharacter _character;
        public AICharacter character => _character ??= GetComponent<AICharacter>();
        
        private AgentAuthoring _agentAuthoring;
        public AgentAuthoring agent => _agentAuthoring ??= GetComponent<AgentAuthoring>();

        public bool haveDestination  { get; private set; } = false;
        public Vector3 destination { get; private set; } = Vector3.zero;

        public Signal<AIMoveResult> onStartMove { get; } = new();
        public Signal<AIMoveResult> onEndMove { get; } = new();
        
        public void SetDestination(Vector3 destination)
        {
            this.destination = destination;
            haveDestination = true;
            agent.SetDestination(destination);
            onStartMove.Dispatch(new AIMoveResult(character, destination, MoveResult.Started));
        }

        public void AbortDestination()
        {
            onEndMove.Dispatch(new AIMoveResult(character, destination, MoveResult.Failed));
            haveDestination = false;
        }
        
        public void ReachDestination()
        {
            onEndMove.Dispatch(new AIMoveResult(character, destination, MoveResult.Reached));
            haveDestination = false;
        }
        
    }
}