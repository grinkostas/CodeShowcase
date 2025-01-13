using System;
using System.Collections.Generic;
using System.Linq;
using Core.Signals.Api;
using UnityEngine.Assertions;

namespace Core.Signals
{
    public abstract partial class ABaseSignal<T>
    {
        protected class Callback : ISignalCallback
        {
            private ABaseSignal<T> _signal;
            
            private List<Func<bool>> _whenPredicate;
            private List<Func<bool>> _offWhenEarlyPredicate;
            private List<Func<bool>> _offWhenLatePredicate;
           
            internal T handler { get; private set; }
            
            public int priority { get; private set; }
            public int countdown { get; private set; }
            
            internal Callback(ABaseSignal<T> signal, T handler)
            {
                Assert.IsNotNull(signal, "Signal can't be null");
                
                _signal = signal;
                this.handler = handler;
                _whenPredicate = null;
                priority = 0;
                countdown = -1;
            }
            
            public bool IsAddedToSignal() => _signal.Has(this);
            public void On() => _signal.On(this);
            public void Once() => _signal.Once(this); 
           
            public void Off()
            {
                if (IsAddedToSignal()) _signal.Off(this);
            }
          
            internal bool CanEmit() => IsAddedToSignal() && IsWhenFulfilled() && IsCountdownFulfilled();

            internal void BeforeTryEmit()
            {
                if (IsOffWhenEarlyFulfilled()) Off();
            }
            
            internal void EmitStart(){}
            
            internal void EmitEnd()
            {
                if (countdown > 0) countdown--;
            }
            
            internal void AfterTryEmit()
            {
                if (IsOffWhenLateFulfilled()) Off();
            }
            
            public ISignalCallback When(Func<bool> predicate) 
            {
                if (_whenPredicate == null) _whenPredicate = new List<Func<bool>>();
                _whenPredicate.Add(predicate);
                return this;
            }
            
            public ISignalCallback OffWhen(Func<bool> predicate, bool late = true) 
            {
                if (late)
                {
                    if (_offWhenLatePredicate == null) _offWhenLatePredicate = new List<Func<bool>>();
                    _offWhenLatePredicate.Add(predicate);
                }
                else
                {
                    if (_offWhenEarlyPredicate == null) _offWhenEarlyPredicate = new List<Func<bool>>();
                    _offWhenEarlyPredicate.Add(predicate);
                }

                return this;
            }

            public ISignalCallback Priority(int value)
            {
                priority = value;
                _signal.Sort();
                return this;
            }

            public ISignalCallback Countdown(int value)
            {
                countdown = value;
                return this;
            }

            private bool IsWhenFulfilled()
            {
                return _whenPredicate == null || _whenPredicate.All(when => when());
            }

            private bool IsOffWhenEarlyFulfilled()
            {
                return _offWhenEarlyPredicate != null && _offWhenEarlyPredicate.All(when => when());
            }

            private bool IsOffWhenLateFulfilled()
            {
                return _offWhenLatePredicate != null && _offWhenLatePredicate.All(when => when());
            }
            
            private bool IsCountdownFulfilled()
            {
                return countdown != 0;
            }
        }
    }
}