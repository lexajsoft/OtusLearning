using Inventory;
using Inventory.Components;

namespace Extension
{
    public static class ItemExtension
    {
        public static EquipableComponent GetEquipableComponent(this Item item)
        {
            if (item.HasComponent<EquipableComponent>())
                return item.GetComponent<EquipableComponent>();
            return null;
        }
        public static bool IsHasEquipableComponent(this Item item)
        {
            return item.HasComponent<EquipableComponent>();
        }
    }
}