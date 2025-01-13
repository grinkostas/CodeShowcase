using Game.Characters.AI;

namespace Game.Characters.AI.Notifications
{
    public abstract class ReactCondition
    {
        public abstract bool CanReact(AINotifier notifier, AICharacter character);
    }
}