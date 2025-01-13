using Core.Signals;

namespace Game.Interact.Api
{
    public interface IInteractItem : IItem
    {
        Signal onStartInteract { get; }
        Signal onStopInteract { get; }

        void Interact();
        void StopInteract();
    }
}