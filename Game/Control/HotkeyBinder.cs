using UnityEngine;

namespace Game.Control
{
    public class HotkeyBinder : MonoBehaviour
    {
        [SerializeField] private KeyCode _keyCode;

        private IHotkeyUser _hotkeyUser;
        public IHotkeyUser hotkeyUser => _hotkeyUser ??= GetComponent<IHotkeyUser>();
        
        private void Update()
        {
            if (Input.GetKeyDown(_keyCode))
            {
                Debug.Log("OnPress");
                hotkeyUser?.OnPress();
            }

            if (Input.GetKeyUp(_keyCode))
            {
                Debug.Log("On Repeal");
                hotkeyUser?.OnRepel();
            }
        }
    }
}