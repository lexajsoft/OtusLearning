using System;

namespace HomeWorkTask2
{
    [Serializable]
    public class Item
    {
        public string ID = "";
        public string Name= "";
        public string IconName= "";
        public long Count = 0;
        public ItemType ItemType = ItemType.None;

        public Item()
        {
            
        }

        public Item(ItemCountRandom itemCountRandom)
        {
            ID = itemCountRandom.ID;
            Name = itemCountRandom.Name;
            IconName = itemCountRandom.IconName;
            ItemType = itemCountRandom.ItemType;
            Count = itemCountRandom.GetRandomCount();
        }
    }
}