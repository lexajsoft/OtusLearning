namespace Inventory.Components.Effects
{
    public interface IEffectAction
    {
        public void Use(Item item);
        public string GetDescription();
    }
}