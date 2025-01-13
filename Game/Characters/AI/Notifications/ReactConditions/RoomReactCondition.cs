using System.Collections.Generic;
using Game.Levels;

namespace Game.Characters.AI.Notifications.ReactConditions
{
    public class RoomReactCondition : ReactCondition
    {
        private List<RoomConfig> _configs;
        
        public RoomReactCondition(List<RoomConfig> rooms)
        {
            _configs = new List<RoomConfig>(rooms);
        }
        
        public override bool CanReact(AINotifier notifier, AICharacter character)
        {
            foreach (var config in _configs)
            {
                if (_configs.Contains(config))
                    return true;
            }

            return false;
        }
    }
}