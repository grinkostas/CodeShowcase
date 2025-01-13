using UnityEngine;

namespace Game.Characters.AI.Notifications.ReactConditions
{
    public class RandomReactCondition : ReactCondition
    {
        public float chance { get; }

        public RandomReactCondition(float chance)
        {
            this.chance = Mathf.Clamp(chance, 0, 100);
        }
        
        public override bool CanReact(AINotifier notifier, AICharacter character)
        {
            return Random.Range(0, 100) <= chance;
        }
    }
}