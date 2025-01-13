using Core.Signals.Api;

namespace Core.Signals
{
    public partial class ABaseSignal<T>
    {
        public ISignalCallback Once(T handler)
        {
            return _On(handler).Countdown(1);
        }
        
        public ISignalCallback Once(T handler, bool single)
        {
            if (single && Has(handler)) return Get(handler);
            
            return Once(handler);
        }
        
        private ISignalCallback Once(Callback callback)
        {
            return _On(callback).Countdown(1);
        }

        public ISignalCallback Once(bool isOn, T handler)
        {
            return isOn ? Once(handler) : Off(handler);
        }

        public ISignalCallback Once(bool isOn, T handler, bool single)
        {
            return isOn ? Once(handler, single) : Off(handler);
        }
    }
}