using System;
using UnityEngine;

namespace Inventory.Components
{
    [Serializable]
    public class StackableComponent : ItemComponent
    {
        [field: SerializeField] public int currentCount { get; private set; }
        [field: SerializeField] public int maxCount { get; private set; }

        public StackableComponent()
        {
            currentCount = 1;
            maxCount = 10;
        }
        public StackableComponent(int currentCount, int maxCount)
        {
            this.currentCount = currentCount;
            this.maxCount = maxCount;
        }

        public bool IsAny => currentCount > 0;
        
        
        // возвращает количество которое не уместилось
        public int Add(int count)
        {
            if (currentCount + count <= maxCount)
            {
                currentCount += count;
                PrintLog();
                Owner.OnItemUpdated?.Invoke(Owner);
                return 0;
            }
            else
            {
                var lost = count - (maxCount - currentCount);
                currentCount = maxCount;
                PrintLog();
                Owner.OnItemUpdated?.Invoke(Owner);
                return lost;
            }
            
        }

        public bool IsCanMinus(int count)
        {
            return currentCount >= count;
        }

        // возвращает количество которое не уместилось
        public void Minus(int count)
        {
            currentCount -= count;
            if (currentCount <= 0)
            {
                currentCount = 0;
                PrintLog();
                Owner.RequestToDestroy();
            }
            else
            {
                PrintLog();
                Owner.ItemUpdated();
            }
            
            
        }

        private void PrintLog()
        {
            Debug.Log($"Стак [{Owner.name}] предмета изменился [{currentCount}/{maxCount}] "); 
        }
    }
}