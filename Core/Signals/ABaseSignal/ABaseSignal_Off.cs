using System;
using Core.Signals.Api;

namespace Core.Signals
{
    public abstract partial class ABaseSignal<T>
    {
        private void __Off(T handler)
        {
            _toRemove.InsertRange(0, _callbacks.FindAll(callback => callback.handler.Equals(handler)));
            
            _toRemove.ForEach(c => _callbacks.Remove(c));
            _toRemove.Clear();
        }
        
        private ISignalCallback _Off(T handler)
        {
            var callback = Get(handler);

            if (!_isDispatching) __Off(handler);
            else _callAfterDispatch.Add(() => __Off(handler));

            return callback;
        }
        
        private ISignalCallback _Off(Callback callback)
        {
            if (!Has(callback)) throw new Exception("Signal does not contain this callback.");

            if (!_isDispatching) _callbacks.Remove(callback);
            else _callAfterDispatch.Add(() => _callbacks.Remove(callback));

            return callback;
        }
        
        public ISignalCallback Off(T handler)
        {
            return _Off(handler);
        }

        private ISignalCallback Off(Callback callback)
        {
            return _Off(callback);
        }

        private void _OffAll()
        {
            _callbacks.Clear();
        }

        public void OffAll()
        {
            if (!_isDispatching) _OffAll();
            else _callAfterDispatch.Add(_OffAll);
        }
    }
}