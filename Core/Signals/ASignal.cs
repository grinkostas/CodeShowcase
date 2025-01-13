using System;

namespace Core.Signals
{
    public abstract class ASignal : ABaseSignal<Action>
    {
        public void Dispatch()
        {
            DispatchInternal(callback => callback.handler());

            if (hub != null)
            {
                if (dispatchToParent && hub.parent != null)
                {
                    hub.parent.DispatchToHash(hash);
                }
                
                if (hub.propagateToSubs && hub.subs is {Count: > 0})
                {
                    hub.subs.ForEach(sub =>
                    {
                        sub.DispatchToHash(hash);
                    });
                }
            }
        }
    }

   
    public abstract class ASignal<T>: ABaseSignal<Action<T>>
    {   
        public void Dispatch(T arg1)
        {
            DispatchInternal(callback => callback.handler(arg1));

            if (hub != null)
            {
                if (dispatchToParent && hub.parent != null)
                {
                    hub.parent.DispatchToHash(hash, arg1);
                } 
                
                if (hub.propagateToSubs && hub.subs is {Count: > 0})
                {
                    hub.subs.ForEach(sub =>
                    {
                        sub.DispatchToHash(hash, arg1);
                    });
                }
            }
        }
    }
    
    public abstract class ASignal<T, U>: ABaseSignal<Action<T, U>>
    {
        public void Dispatch(T arg1, U arg2)
        {
            DispatchInternal(callback => callback.handler(arg1, arg2));

            if (hub != null)
            {
                if (dispatchToParent && hub.parent != null)
                {
                    hub.parent.DispatchToHash(hash, arg1, arg2);
                }
                
                if (hub.propagateToSubs && hub.subs is {Count: > 0})
                {
                    hub.subs.ForEach(sub =>
                    {
                        sub.DispatchToHash(hash, arg1, arg2);
                    });
                }
            }
        }
    }

    public abstract class ASignal<T, U, V>: ABaseSignal<Action<T, U, V>>
    {
        public void Dispatch(T arg1, U arg2, V arg3)
        {
            DispatchInternal(callback => callback.handler(arg1, arg2, arg3));

            if (hub != null)
            {
                if (dispatchToParent && hub.parent != null)
                {
                    hub.parent.DispatchToHash(hash, arg1, arg2, arg3);
                }
                
                if (hub.propagateToSubs && hub.subs is {Count: > 0})
                {
                    hub.subs.ForEach(sub =>
                    {
                        sub.DispatchToHash(hash, arg1, arg2, arg3);
                    });
                }
            }
        }
    }
}