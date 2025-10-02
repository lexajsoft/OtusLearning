using System;
using UnityEngine;

namespace Inventory.Components
{
    public interface IDurable : IItemComponent
    {
        int MaxDurability { get; }
        int CurrentDurability { get; }
        void ReduceDurability(int amount);
        bool IsBroken { get; }
    }

    [Serializable]
    public class DurabilityComponent : ItemComponent, IDurable
    {
        [field:SerializeField] public int MaxDurability{ get; private set; } = 100;
        [field: SerializeField] public int CurrentDurability { get; private set; } = 100;
        public bool IsBroken => CurrentDurability <= 0;
        public event Action OnItemBroken;

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
            CurrentDurability = Mathf.Max(0, CurrentDurability - amount);
        
            if (IsBroken)
            {
                OnItemBroken?.Invoke();
            }
        }
    
        public void Repair(int amount)
        {
            CurrentDurability = Mathf.Min(MaxDurability, CurrentDurability + amount);
        }
    
        
    }
}