using Game.Levels;
using Core.Settings;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(menuName = "Settings/Game", fileName = "GameSettings")]
    public class GameSettings : Settings<GameSettings>
    {
        public LevelConfig levelConfig;
    }
}