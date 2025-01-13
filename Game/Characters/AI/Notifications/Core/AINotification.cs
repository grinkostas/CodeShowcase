using System.Collections.Generic;

namespace Game.Characters.AI.Notifications
{
    public class AINotification
    {
        public int priority { get; }
        public AINotifier notifier { get; }
        
        protected AINotificationAction _action;
        private List<ReactCondition> _reactConditions;

        public AINotification(AINotifier notifier, int priority = 0, List<ReactCondition> conditions = null)
        {
            this.priority = priority;
            _reactConditions = conditions ?? new List<ReactCondition>();
            this.notifier = notifier;
        }

        public AINotification AddReactCondition(ReactCondition condition)
        {
            if(_reactConditions.Contains(condition) == false && condition != null)
                _reactConditions.Add(condition);
            return this;
        }

        public bool NeedToReact(AICharacter character)
        {
            foreach (var condition in _reactConditions)
            {
                if (condition.CanReact(notifier, character) == false)
                    return false;
            }
            return true;
        }
        
        public AINotification SetAction(AINotificationAction action)
        {
            _action = action;
            return this;
        }

        public void React(AICharacter character)
        {
            _action?.React(this, character);
        }
    }
}