using DG.Tweening;
using NaughtyAttributes;
using Core.Utilities;
using UnityEngine;
using Zenject;

namespace Core.Utilities
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField] private Vector3 _direction = Vector3.forward;
        [SerializeField] private bool _actualizeInUpdate;
        [SerializeField] private UpdateType _updateType = UpdateType.Normal;
        
        private Camera _camera;
        public Camera cam => _camera ??= Camera.main;

        private bool _injected = false;
        
        [Inject]
        private void OnInject()
        {
            _injected = true;
            Actualize();
        }
        

        private void Update()
        {
            if(_injected && _actualizeInUpdate && _updateType == UpdateType.Normal)
                Actualize();
        }

        private void FixedUpdate()
        {
            if(_injected && _actualizeInUpdate && _updateType == UpdateType.Fixed)
                Actualize();
        }

        private void LateUpdate()
        {
            if(_injected && _actualizeInUpdate && _updateType == UpdateType.Late)
                Actualize();
        }

        public void Actualize()
        {
            var cameraTransform = cam.transform;
            transform.LookAt(transform.position + cameraTransform.rotation * _direction, cameraTransform.rotation * Vector3.up);
        }

        [Button("Actualize")]
        private void EditorActualize()
        {
            if (Camera.main != null)
                transform.LookAt(transform.position + Camera.main.transform.rotation * _direction,
                    Camera.main.transform.rotation * Vector3.up);
        }
    }
}
