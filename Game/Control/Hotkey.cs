using UnityEngine;
using UnityEngine.Events;

namespace Game.Control
{
    public class Hotkey : MonoBehaviour
    {
        [SerializeField] private KeyCode _keyCode;
        
        public UnityEvent onKeyUp;
        public UnityEvent onKey;
        public UnityEvent onKeyDown;
        
        private void Update()
        {
            if (Input.GetKey(_keyCode))
            {
                onKey?.Invoke();
            }
            else if (Input.GetKeyDown(_keyCode))
            {
                onKeyDown?.Invoke();
            }
            else if (Input.GetKeyUp(_keyCode))
            {
                onKeyUp?.Invoke();
            }
        }
    }
}