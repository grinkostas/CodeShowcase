namespace Game.Characters.AI.Notifications
{
    public class AIMoveAction : AINotificationAction
    {
        public void React(AINotification notification, AICharacter character)
        {
            character.movement.SetDestination(notification.notifier.position);
        }
    }
}