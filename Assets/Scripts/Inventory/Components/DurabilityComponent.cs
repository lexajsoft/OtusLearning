using System;
using UnityEngine;

namespace Inventory.Components
{
    [Serializable]
    public class DurabilityComponent : ItemComponent, IDurable
    {
        [field:SerializeField] public int MaxDurability{ get; private set; } = 100;
        [field: SerializeField] public int CurrentDurability { get; private set; } = 100;
        public bool IsBroken => CurrentDurability <= 0;
        
        public DurabilityComponent()
        {
            
        }

        public DurabilityComponent(int currentDurability, int maxDurability)
        {
            CurrentDurability = currentDurability;
            MaxDurability = maxDurability;
        }

        public void ReduceDurability(int amount)
        {
            if (CurrentDurability == 0)
                return;
            
            CurrentDurability = Mathf.Max(0, CurrentDurability - amount);
            Debug.Log($"Предмет поломало - [{Owner.id}] [{Owner.name}]  - [{CurrentDurability}/{MaxDurability}]");
            if (IsBroken)
            {
                Debug.Log($"Предмет СЛОМАН - [{Owner.id}] [{Owner.name}]");
                // если вещь сломалась то мы ее не удаляем теперь
                //Owner.RequestToDestroy();
            }
            Owner.ItemUpdated();
        }
    
        public void Repair(int amount)
        {
            if(amount <= 0)
                return;
            
            CurrentDurability = Mathf.Min(MaxDurability, CurrentDurability + amount);
            Debug.Log($"Предмет починили - [{Owner.id}] [{Owner.name}]  - [{CurrentDurability}/{MaxDurability}]");
            Owner.ItemUpdated();
        }
    
        
    }
}