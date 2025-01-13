namespace Game.Interact.Api
{
    public interface IPickUpItem : IItem
    {
        public void PickUp();
        public void Drop();
    }
}