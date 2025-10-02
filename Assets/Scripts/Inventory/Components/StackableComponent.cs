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
    }
}