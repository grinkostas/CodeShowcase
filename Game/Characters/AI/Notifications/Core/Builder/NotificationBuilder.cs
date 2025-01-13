using System.Collections.Generic;
using Game.Characters.AI.Notifications.ReactConditions;
using Game.Levels;

namespace Game.Characters.AI.Notifications
{
    public class NotificationBuilder
    {
        private AINotification _notification;
            
        public NotificationBuilder(AINotifier notifier, int priority)
        {
            _notification = new AINotification(notifier, priority);    
        }
            
        public NotificationBuilder AddDistanceCondition(float distance)
        {
            _notification.AddReactCondition(new DistanceReactCondition(distance));
            return this;
        }
            
        public NotificationBuilder AddRandomCondition(float chance)
        {
            _notification.AddReactCondition(new RandomReactCondition(chance));
            return this;
        }
            
        public NotificationBuilder AddRoomCondition(List<RoomConfig> rooms)
        {
            _notification.AddReactCondition(new RoomReactCondition(rooms));
            return this;
        }

        public AINotification Get(AINotificationAction action) => _notification.SetAction(action);
    }
}