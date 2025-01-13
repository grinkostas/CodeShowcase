using UnityEngine;

namespace Game.Levels
{
    [CreateAssetMenu(menuName = "Configs/Game/room", fileName = "RoomConfig")]
    public class RoomConfig : ScriptableObject
    {
        public string id;
    }
}