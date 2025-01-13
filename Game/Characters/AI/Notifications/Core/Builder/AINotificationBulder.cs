using System.Collections.Generic;
using Game.Data;
using Game.Levels;
using JetBrains.Annotations;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Game.Characters.AI.Notifications
{
    public class AINotificationBuilder : MonoBehaviour, AINotifier
    {
        [SerializeField] private int _priotiry;
        [SerializeField] private RoomConfig _room;
        [Header("DefaultSettings")] 
        [SerializeField] private  bool _haveRoomCondition = false;
        [SerializeField, ShowIf(nameof(_haveRoomCondition))] private List<RoomConfig> _rooms;
        [SerializeField] private bool _haveDistanceCondition = true;
        [SerializeField, ShowIf(nameof(_haveDistanceCondition))] private float _reactDistance;
        [Space]
        [SerializeField] private bool _haveRandomCondition = false;
        [SerializeField, ShowIf(nameof(_haveRandomCondition))] private float _randomChance;

        [InjectOptional, UsedImplicitly] public AINotificationModule notificationModule { get; }
        
        public LevelConfig level => GameSettings.data.levelConfig;
        public RoomConfig room { get; }
        public Vector3 position => transform.position;

        [Button]
        private void Notify()
        {
            notificationModule.Notify(GetDefault());
        }

        public AINotification GetDefault()
        {
            var builder = new NotificationBuilder(this, _priotiry);

            if (_haveDistanceCondition)
                builder.AddDistanceCondition(_reactDistance);
            
            if (_haveRandomCondition)
                builder.AddRandomCondition(_randomChance);
            
            if(_haveRoomCondition)
                builder.AddRoomCondition(_rooms);

            return builder.Get(new AIMoveAction());
        }
        
        public NotificationBuilder GetTemplate()
        {
            return new NotificationBuilder(this, _priotiry);
        }

        public NotificationBuilder GetTemplate(int priority)
        {
            return new NotificationBuilder(this, priority);
        }
        
        public NotificationBuilder GetTemplate(AINotifier notifier, int priority)
        {
            return new NotificationBuilder(notifier, priority);
        }
    }
}