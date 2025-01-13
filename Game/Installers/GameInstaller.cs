using Game.Characters.AI.Notifications;
using Core.Utilities;
using UnityEngine;
using Zenject;

namespace GameCore.Game.Scripts.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private InjectStarter _injectStarter;
        
        public override void InstallBindings()
        {
            Container.Bind<InjectStarter>().FromInstance(_injectStarter).AsSingle();
            Container.BindInterfacesAndSelfTo<AINotificationModule>().AsSingle().NonLazy();
        }
    }
}