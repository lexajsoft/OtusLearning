using System;
using UnityEngine;

namespace Inventory.Components
{
    
    public interface IEquipable : IItemComponent
    {
        EquipSlot Slot { get; }
        void OnEquip();
        void OnUnequip();
    }
    
    [Serializable]
    public class EquipableComponent : ItemComponent, IEquipable
    {
        [field: SerializeField] public EquipSlot Slot{ get; private set; }
        public bool IsEquiped;
        public EquipableComponent()
        {
            Slot = EquipSlot.NONE;
        }

        public EquipableComponent(EquipSlot equipSlot)
        {
            Slot = equipSlot;
        }
        
        public void OnEquip()
        {
            
        }

        public void OnUnequip()
        {
            
        }
    }
}