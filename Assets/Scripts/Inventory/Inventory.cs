using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory
{
    [Serializable]
    public class Inventory
    {
        [field: SerializeField] public List<Item> items { get; private set; } = new List<Item>();

        public void AddItem(Item item)
        {
            items.Add(item);
            Debug.Log($"Добавлен предмет в инвентарь {item.id}-{item.name}");
        }
        
        public void RemoveItem(Item item)
        {
            items.Remove(item);
            Debug.Log($"Удален предмет в инвентарь {item.id}-{item.name}");
        }

        public void RemoveAll()
        {
            items.Clear();
                
        }
    }
}
