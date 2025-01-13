using Core.Signals;

namespace Game.Interact.Api
{
    public interface IPointerInteractItem : IInteractItem
    {
        Signal<IInteractor> onPointerInteract { get; }

        void Interact(IInteractor interactor);
    }
}