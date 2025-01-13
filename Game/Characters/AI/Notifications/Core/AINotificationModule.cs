using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.AI.Notifications
{
    public class AINotificationModule
    {
        private List<AINotificationListener> _notificationListeners { get; }

        public AINotificationModule()
        {
            _notificationListeners = new();
        }
        
        public void Add(AINotificationListener listener)
        {
            _notificationListeners.Add(listener);
        }
        
        public void Notify(AINotification notification)
        {
            Debug.Log($"On Notify");
            foreach (var listener in _notificationListeners)
            {
                Debug.Log(notification.NeedToReact(listener.character));
                if (notification.NeedToReact(listener.character) == false)
                    continue;
                notification.React(listener.character);
            }
        }
    }
}