using Game.Levels;
using UnityEngine;

namespace Game.Characters.AI.Notifications
{
    public interface AINotifier
    {
        public LevelConfig level { get; }
        public RoomConfig room { get; }
        public Vector3 position { get; }
    }
}