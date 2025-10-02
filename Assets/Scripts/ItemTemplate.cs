using System;
using System.Collections.Generic;
using Inventory;
using Inventory.Components;
using UnityEngine;

[CreateAssetMenu(menuName = "Create ItemTemplate", fileName = "ItemTemplate", order = 0)]
[Serializable]
public class ItemTemplate : ScriptableObject
{
    public int ItemID;
    public string ItemName;
    public LevelRare LevelRare;
    
    [SerializeReference]public List<ItemComponent> Components;
    
    public Item CreateItem()
    {
        var item = new Item(ItemID, ItemName);
        foreach (var component in Components)
        {
            item.AddComponent(component);
        }
        return item;
    }
}