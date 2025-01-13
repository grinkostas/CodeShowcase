using System;
using Core.Signals.Api;

namespace Core.Signals
{
    public abstract partial class ABaseSignal<T>
    {
        private ISignalCallback _On(Callback callback)
        {
            if (Has(callback)) throw new Exception("Signal already contains this callback.");
            
            if (!_isDispatching)
            {
                _callbacks.Add(callback);
                Sort();
            }
            else
            {
                _callAfterDispatch.Add(() =>
                {
                    _callbacks.Add(callback);
                    Sort();
                });
            }
            
            return callback;
        }
        
        private ISignalCallback _On(T handler)
        {
            return _On(new Callback(this, handler));
        }

        public ISignalCallback On(T handler)
        {
            return _On(handler);
        }

        private ISignalCallback On(Callback callback)
        {
            return _On(callback);
        }
        
        public ISignalCallback On(T handler, bool single)
        {
            if (single && Has(handler)) return Get(handler);
            
            return On(handler);
        }

        public ISignalCallback On(bool isOn, T handler)
        {
            return isOn ? On(handler) : Off(handler);
        }
        
        public ISignalCallback On(bool isOn, T handler, bool single)
        {
            return isOn ? On(handler, single) : Off(handler);
        }
    }
}