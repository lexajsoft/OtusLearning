using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HomeWorkTask2
{
    // Класс отвечающий за рандомизацию падающего предмета
    [Serializable]
    public class ItemCountRandom : Item
    {
        [Range(0.00001f,100f)]public float Chance;
        public int MinCount = 1;
        public int MaxCount = 1;

        public int GetRandomCount() => Random.Range(MinCount, MaxCount);

        public Item GetItem()
        {
            return new Item(this);
        }

        public bool IsCanGet(float chance)
        {
            return Chance >= chance;
        }
    }
}