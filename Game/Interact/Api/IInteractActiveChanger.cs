using Core.Signals;

namespace Game.Interact.Api
{
    public interface IInteractActiveChanger
    {
        public Signal<bool> onChangeActiveStatus { get; }
    }
}