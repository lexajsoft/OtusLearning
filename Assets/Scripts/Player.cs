using System;
using Inventory;
using Inventory.Components;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;

[Serializable]
public class Player
{
    public Inventory.Inventory inventory;
    [Inject] private ItemGenerator _itemGenerator;
    
    public Player()
    {
        inventory = new Inventory.Inventory();
        
    }

    public void CreateRandomInventory()
    {
        int levelRangeInt = Random.Range(0, 5);
        Debug.Log("levelRangeInt:" + levelRangeInt);
        LevelRare levelRare = (LevelRare)levelRangeInt;
        inventory.AddItem(_itemGenerator.CreateItemEquipableItem(EquipSlot.Head, levelRare));
        inventory.AddItem(_itemGenerator.CreateItemEquipableItem(EquipSlot.Arms, levelRare));
        inventory.AddItem(_itemGenerator.CreateItemEquipableItem(EquipSlot.Chest, levelRare));
        inventory.AddItem(_itemGenerator.CreateItemEquipableItem(EquipSlot.Feet, levelRare));
        inventory.AddItem(_itemGenerator.CreateItemEquipableItem(EquipSlot.Weapon, levelRare));
    }
}