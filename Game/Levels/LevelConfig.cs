using System.Collections.Generic;
using UnityEngine;

namespace Game.Levels
{
    [CreateAssetMenu(menuName = "Configs/Game/Level", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public string id;
        public List<RoomConfig> rooms;
    }
}