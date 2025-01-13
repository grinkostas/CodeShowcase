using System;
using System.Collections.Generic;
using Core.Signals.Api;

namespace Core.Signals
{
    public abstract partial class ABaseSignal<T> : ISignal
    {
        private readonly List<Callback> _callbacks = new List<Callback>();
        private readonly List<Callback> _callbacksIterate = new List<Callback>();
        private readonly List<Callback> _toRemove = new List<Callback>();
        private readonly List<Action> _callAfterDispatch = new List<Action>();
        
        private SignalHub _hub;
        
        private string _hash;
        private bool _isDispatching;
        
        public int count => _callbacks.Count;

        public int dispatchCount { get; private set; } = 0;
        public bool dispatchedAtLeastOnce => dispatchCount > 0;
        
        public SignalHub hub
        {
            get => _hub;
            set 
            {
                if (_hub != null) throw new Exception("Hub can be set only once. From SignalHub.");
                _hub = value; 
            } 
        }

        public bool dispatchToParent { get; set; }
        
        public string hash
        {
            get
            {
                if (string.IsNullOrEmpty(_hash)) _hash = GetType().ToString();
                return _hash;
            }
        }
        
        public bool Has(T handler)
        {
            return _callbacks.Exists(callback => callback.handler.Equals(handler));
        }
        
        protected bool Has(Callback callback)
        {
            return _callbacks.Contains(callback);
        }

        protected Callback Get(T handler)
        {
            return _callbacks.Find(c => Equals(c.handler, handler));
        }
       
        public void Sort()
        {
            _callbacks.Sort((a, b) =>
            {
                if (a.priority > b.priority) return -1;
                if (a.priority < b.priority) return 1;
                return 0;
            });
        }

        protected void DispatchInternal(Action<Callback> externalDispatch)
        {
            dispatchCount++;
            if (_callbacks.Count > 0) 
            {
                _callbacksIterate.Clear();
                _callbacks.ForEach(_callbacksIterate.Add);
                _callbacksIterate.ForEach(callback =>
                {
                    callback.BeforeTryEmit();
                    if (callback.CanEmit())
                    {
                        callback.EmitStart();
                        externalDispatch.Invoke(callback);
                        callback.EmitEnd();
                    }
                    callback.AfterTryEmit();
                });
            }
            
            for (var i = _callbacks.Count - 1; i >= 0; i--)
            {
                if (_callbacks[i].countdown == 0) _callbacks.RemoveAt(i);
            }
            
            if (_callAfterDispatch.Count > 0) {
                _callAfterDispatch.ForEach(action => action());
                _callAfterDispatch.Clear();
            }
        }
    }
}