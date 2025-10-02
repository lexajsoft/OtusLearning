using System;

namespace Inventory.Components
{
    [Serializable]
    public abstract class ItemComponent : IItemComponent
    {
        public Item Owner { get; private set; }
    
        public virtual void Initialize(Item owner)
        {
            Owner = owner;
        }
    }
}