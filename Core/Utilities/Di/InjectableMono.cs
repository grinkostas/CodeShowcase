using System.Collections.Generic;
using JetBrains.Annotations;
using Core.Signals;
using Core.Signals.Api;
using UnityEngine;
using Zenject;

namespace Core.Utilities
{
    public class InjectableMono : MonoBehaviour
    {
        [Inject, UsedImplicitly] private InjectStarter _injectStarter;
        
        public bool injected { get; private set; } = false;
        public bool destroyed { get; private set; } = false;

        public Signal onInjected { get; } = new();
        
        private List<ISignalCallback> _callbacks = new();
        
        [Inject]
        private void Inject()
        {
            _injectStarter.Add(this);
            injected = true;
            OnInject();
            onInjected.Dispatch();
        }
        protected virtual void OnInject(){}
        
        public virtual void InjectAwake(){}
        public virtual void InjectStart(){}

        public void InjectComplete()
        {
            injected = true;
            OnInjectComplete();
            _callbacks = GetListeners();
        }

        protected virtual void OnEnable()
        {
            if (injected)
                _callbacks = GetListeners();
        }

        protected virtual void OnDisable()
        {
            if(_callbacks is not { Count: > 0 })
                return;

            OffCallbacks();
        }

        protected void OffCallbacks()
        {
            _callbacks.ForEach(x=>x?.Off());
        }

        protected virtual List<ISignalCallback> GetListeners() => new();
        protected virtual void OnInjectComplete(){}
    }
}