using Game.Characters;
using Game.Interact.Api;

namespace Game.Interact
{
    public enum InteractorType
    {
        All,
        MainCharacter,
        CharacterDrone
    }
    
    public static class InteractorExtensions
    {
        public static bool Compare(this InteractorType interactorType, IInteractor interactor)
        {
            switch (interactorType)
            {
                case InteractorType.MainCharacter:
                    return interactor is Player;
                case InteractorType.CharacterDrone:
                    return interactor is PlayerDrone;
                default:
                    return true;
            }
        }
    }
}