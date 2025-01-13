using System;

namespace Core.Signals.Api
{
    public interface ISignalCallback
    {
        int priority { get; }
        int countdown { get; }
     
        bool IsAddedToSignal();
      
        void On();
        void Once();
        void Off();
      
        ISignalCallback When(Func<bool> predicate);
        ISignalCallback OffWhen(Func<bool> predicate, bool late = true);
        
        ISignalCallback Priority(int value);
        ISignalCallback Countdown(int value);
    }
}