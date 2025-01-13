using DG.Tweening;
using Game.Characters;
using Game.Interact.Api;
using Game.Interact.Configs;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Interact
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class PickUpPointerInteractItem : PointerInteractItem, IPickUpItem, IThrowItem
    {
        [SerializeField] private PickUpItemConfig _itemConfig;
        [SerializeField] private bool _overridePickedLocalPosition;
        [SerializeField, ShowIf(nameof(_overridePickedLocalPosition))] private Vector3 _pickedLocalPosition = Vector3.zero;
        [SerializeField] private bool _overridePickedLocalRotation;
        [SerializeField, ShowIf(nameof(_overridePickedLocalRotation))] private Vector3 _pickedLocalRotation = Vector3.zero;
        [SerializeField] private bool _overridePickedLocalScale;
        [SerializeField, ShowIf(nameof(_overridePickedLocalScale))] private Vector3 _pickedLocalScale = Vector3.one;

        private float _tweensTime = 0.35f;
        public GameObject itemGO => gameObject;
        public PickUpItemConfig config => _itemConfig;
        
        private Rigidbody _rbCached;
        public Rigidbody rb => _rbCached ??= GetComponent<Rigidbody>();
        
        private Collider _colliderCached;
        public Collider col => _colliderCached ??= GetComponent<Collider>();

        private Vector3 _defaultScale;
        private Tween _scaleTween;
        private Tween _moveTween;
        private Tween _rotateTween;

        private void Awake()
        {
            _defaultScale = transform.localScale;
        }

        private void OnEnable()
        {
            onPointerInteract.On(OnPointerInteract);
            onPointerStopInteract.On(OnPointerStopInteract);
        }

        private void OnDisable()
        {
            onPointerInteract.Off(OnPointerInteract);
            onPointerStopInteract.Off(OnPointerStopInteract);
        }

        private void OnPointerInteract(IInteractor interactor)
        {
            if (interactor is Player player)
            {
                player.pickUpItemModule.TryPickUpItem(this);
            }
        }
        
        private void OnPointerStopInteract(IInteractor interactor)
        {
            if (interactor is Player player)
            {
                player.pickUpItemModule.ThrowItem(this);
            }
        }

        public void PrepareForThrow()
        {
            EnablePhysics();
        }

        public void Throw()
        {
            
        }
        
        public void Drop()
        {
            EnablePhysics();
            if (_moveTween != null) _moveTween.Kill();
            if (_rotateTween != null) _rotateTween.Kill();
            if (_overridePickedLocalScale)
            {
                if (_scaleTween != null) _scaleTween.Kill();
                _scaleTween = transform.DOScale(_defaultScale, _tweensTime).SetEase(Ease.Linear).SetLink(gameObject);
            }
        }

        private void EnablePhysics()
        {
            transform.SetParent(null);
            col.enabled = true;
            rb.useGravity = true;
            rb.isKinematic = false;
        }

        public void PickUp()
        {
            DisablePhysics();
            _moveTween = transform.DOLocalMove(_pickedLocalPosition, _tweensTime).SetEase(Ease.Linear).SetLink(gameObject);
            _rotateTween = transform.DOLocalRotate(_pickedLocalRotation, _tweensTime).SetEase(Ease.Linear).SetLink(gameObject);
            if (_overridePickedLocalScale)
            {
                if (_scaleTween != null) _scaleTween.Kill();
                _scaleTween = transform.DOScale(_pickedLocalScale, _tweensTime).SetEase(Ease.Linear).SetLink(gameObject);
            }
        }
        
        private void DisablePhysics()
        {
            col.enabled = false;
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }
}