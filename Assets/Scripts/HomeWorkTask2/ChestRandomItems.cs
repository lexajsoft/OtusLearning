using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace HomeWorkTask2
{
    [Serializable]
    public class ChestRandomItems
    {
        public List<ItemCountRandom> Items = new List<ItemCountRandom>();

        public List<Item> GetRandomItems()
        {
            List<Item> rewards = new List<Item>();
            
            for (int i = 0; i < Items.Count; i++)
            {
                var chance = Random.Range(0, 1f);
                if (Items[i].IsCanGet(chance))
                {
                    rewards.Add(Items[i].GetItem());
                }
            }

            return rewards;
        }
    }
}