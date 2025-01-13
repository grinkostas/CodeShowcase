namespace Game.Characters.AI.Notifications
{
    public interface AINotificationAction
    {
        void React(AINotification notification, AICharacter character);
    }
}