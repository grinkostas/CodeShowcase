using UnityEngine;

namespace Core.Utilities
{
    public class CanvasWorldPositionLinker : MonoBehaviour
    {
        private Camera _camera;
        public Camera cam => _camera ??= Camera.main;
        
        private RectTransform _rectTransformCashed;

        public RectTransform rectTransform
        {
            get
            {
                if (_rectTransformCashed == null)
                    _rectTransformCashed = GetComponent<RectTransform>();
                return _rectTransformCashed;
            }
        }
        
        private bool _magnet = false;
        private Vector3 _worldPosition;
        
        
        public void Magnet(Vector3 worldPosition)
        {
            _magnet = true;
            _worldPosition = worldPosition;
        }

        public void DisableMagnet()
        {
            _magnet = false;
        }
        
        private void LateUpdate()
        {
            if(_magnet == false)
                return;
            
            Vector2 targetViewportPosition = cam.WorldToViewportPoint(_worldPosition);
            rectTransform.anchorMin = targetViewportPosition;
            rectTransform.anchorMax = targetViewportPosition;
        }
    }
}