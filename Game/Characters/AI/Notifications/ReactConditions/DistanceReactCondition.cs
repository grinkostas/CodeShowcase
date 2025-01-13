using Core.Extentions;

namespace Game.Characters.AI.Notifications.ReactConditions
{
    public class DistanceReactCondition : ReactCondition
    {
        public float maxDistance { get; }

        public DistanceReactCondition(float reactDistance)
        {
            maxDistance = reactDistance;
        }
            
        public override bool CanReact(AINotifier notifier, AICharacter character)
        {
            return VectorExtensions.SqrDistance(notifier.position, character.transform.position) < maxDistance * maxDistance;
        }
    }
}