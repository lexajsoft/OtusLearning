using System;
using UnityEngine;

namespace Inventory.Components.Effects
{
    [Serializable]
    public class RepairEffect : IEffectAction
    {
        [field:SerializeField] public int Value { get; set; }

        public void Use(Item item)
        {
            if (item.GetOwnerPlayer() != null)
            {
                var items = item.GetOwnerPlayer().inventory.items;
                for (int i = 0; i < items.Count; i++)
                {
                    items[i].TryRepairArmorOrWeapon(Value);
                }
            }
            else
            {
                Debug.LogError("Использование эффекта не возможно так как не понятно кто использует");
            }
        }

        public string GetDescription()
        {
            return $"Чинит все предметы в инвентаре на {Value}";
        }
    }
}