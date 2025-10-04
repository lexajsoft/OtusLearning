using System;
using UnityEngine;

namespace Inventory.Components.Effects
{
    [Serializable]
    public class ManaEffect : IEffectAction
    {
        [field:SerializeField]public int Value { get; set; }

        public void Use(Item item)
        {
            if (item.GetOwnerPlayer() != null)
            {
                item.GetOwnerPlayer().Mana.AddResources(Value);
            }
            else
            {
                Debug.LogError("Использование эффекта не возможно так как не понятно кто использует");
            }
        }

        public string GetDescription()
        {
            return $"Востанавливает ману на {Value}";
        }
    }
}