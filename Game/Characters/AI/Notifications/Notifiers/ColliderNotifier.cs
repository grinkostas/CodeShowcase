using System.Collections.Generic;
using DG.Tweening;
using Core.Utilities.Enums;
using JetBrains.Annotations;
using Core.Extentions;
using Core.Signals;
using Core.Signals.Api;
using Core.Utilities;
using UnityEngine;
using Zenject;

namespace Game.Characters.AI.Notifications.Notifiers
{
    public class ColliderNotifier : InjectableMono
    {
        [SerializeField] private float _detectionTime;
        [SerializeField] private AINotificationBuilder _notificationBuilder;
        [SerializeField] private HierarchyLocation _colliderLocation;
        
        [InjectOptional, UsedImplicitly] public AINotificationModule notificationModule { get; }
        
        private ReactiveCollider _reactiveCollider;
        public ReactiveCollider reactiveCollider =>
            _reactiveCollider ??= gameObject.GetComponent<ReactiveCollider>(_colliderLocation);

        private bool _isEntered;
        private bool _detected = false;
        
        private float _detectionTimeProgress = 0;
        public float timeProgress 
        {
            get => _detectionTimeProgress;
            set
            {
                float newValue = value.Clamp(0, _detectionTime);
                if(newValue.IsEqual(_detectionTimeProgress))
                    return;
                _detectionTimeProgress = newValue;
                if(_detected)
                    return;
                if (_detectionTimeProgress.IsEqual(_detectionTime))
                    OnDetect();
                onDetecting.Dispatch(new Progress(_detectionTimeProgress, _detectionTime, _detected));
            }
        }
        
        private List<INotifyTarget> _targets = new();
        
        public Signal<Progress> onDetecting { get; } = new();
        public Signal onDetected { get; } = new();
        
        protected override List<ISignalCallback> GetListeners()
        {
            return new()
            {
                reactiveCollider.onTriggerEnter.On(OnEnter),
                reactiveCollider.onTriggerExit.On(OnExit)
            };
        }
        
        private void OnEnter(Collider other)
        {
            if(other.TryGetComponent(out INotifyTarget target) == false)
                return;
            _isEntered = true;
            _targets.Add(target);
        }

        private void FixedUpdate()
        {
            if(_detected)
                return;

            if (_isEntered == false)
            {
                timeProgress -= Time.fixedDeltaTime;
                return;
            }

            timeProgress += Time.deltaTime * _targets.Count;
        }
        
        private void OnDetect()
        {
            _detected = true;
            onDetected.Dispatch();
            var notification = _notificationBuilder.GetDefault();
            notificationModule.Notify(notification);
            DOVirtual.DelayedCall(3f, Restart).SetId(this).SetUpdate(false);
        }

        private void Restart()
        {
            _detected = false;
        }

        private void OnExit(Collider other)
        {
            if(other.TryGetComponent(out INotifyTarget target) == false)
                return;
            _isEntered = false;
            _targets.Remove(target);
        }
    }
}