using UnityEngine;

namespace Game.Characters.Modules
{
    public class PlayerModule : MonoBehaviour
    {
        private Player _player;
        public Player player => _player ??= GetComponentInParent<Player>(true);
    }
}