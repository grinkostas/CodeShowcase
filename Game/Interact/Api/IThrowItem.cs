using Game.Characters.Api;

namespace Game.Interact.Api
{
    public interface IThrowItem : IItem, IRigidBodyUser
    {
        void PrepareForThrow();
        void Throw();
    }
}