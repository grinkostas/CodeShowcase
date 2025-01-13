using UnityEngine;

namespace Game.Levels
{
    public class LevelRoom : MonoBehaviour
    {
        [SerializeField] private RoomConfig _roomConfig;

        public RoomConfig config => _roomConfig;
    }
}