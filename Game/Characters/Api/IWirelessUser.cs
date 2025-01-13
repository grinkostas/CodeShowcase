using Game.Characters.Modules;
using Game.Interact.Api;

namespace Game.Characters.Api
{
    public interface IWirelessUser : IInteractor
    {
        public WirelessModule wirelessModule { get; }
    }
}