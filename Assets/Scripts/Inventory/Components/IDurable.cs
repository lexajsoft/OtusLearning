namespace Inventory.Components
{
    public interface IDurable : IItemComponent
    {
        int MaxDurability { get; }
        int CurrentDurability { get; }
        void ReduceDurability(int amount);
        bool IsBroken { get; }
    }
}