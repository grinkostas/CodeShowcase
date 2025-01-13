using JetBrains.Annotations;
using Core.Utilities;
using UnityEngine;
using Zenject;

namespace Game.Characters.AI.Notifications
{
    public class AINotificationListener : InjectableMono
    {
        [Inject, UsedImplicitly] public AINotificationModule notificationModule { get; }

        private AICharacter _character;
        public AICharacter character => _character ??= GetComponentInParent<AICharacter>(true);

        protected override void OnInject()
        {
            notificationModule.Add(this);
        }
    }
}