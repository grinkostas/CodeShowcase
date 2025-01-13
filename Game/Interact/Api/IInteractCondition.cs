namespace Game.Interact.Api
{
    public interface IInteractCondition
    {
        bool CanInteract(IInteractor interactor);
    }
}