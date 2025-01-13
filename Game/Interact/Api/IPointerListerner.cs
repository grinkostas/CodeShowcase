using Core.Signals;

namespace Game.Interact.Api
{
    public interface IPointerListener
    {
        Signal onPointerEnter { get; }
        Signal onPointerExit { get; }
        
        bool CanInteract(IInteractor interactor);
        
        void OnPointerEnter();
        void OnPointerExit();
    }
}