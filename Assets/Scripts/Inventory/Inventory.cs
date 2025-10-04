using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.Components;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory
{
    [Serializable]
    public class Inventory
    {
        [field: SerializeField] public Dictionary<EquipSlot, Item> equipped = new();
        [field: SerializeField] public List<Item> items { get; private set; } = new List<Item>();

        public Action<Item> OnItemAdded;
        public Action<Item> OnItemRemoved;
        public Action<Item> OnItemUpdated;
        public Action<EquipSlot, Item> OnEquippedItem; 
        public Action<EquipSlot> OnUnEquippedItem; 
        public Action OnInventoryChanged;

        private Player _ownerPlayer;
        public Inventory()
        {
            Debug.Log("Create Inventory");
            
            items = new List<Item>();

            // слоты активные для игрока потом можно поменять или перенести в конфиг на случай если захочется к примеру 
            // сделать игрока который может носить только горшок на голове и катаны
            equipped = new Dictionary<EquipSlot, Item>();
            equipped[EquipSlot.Head] = null;
            equipped[EquipSlot.Chest] = null;
            equipped[EquipSlot.Arms] = null;
            equipped[EquipSlot.Feet] = null;
            equipped[EquipSlot.Weapon] = null;
        }

        public void SetPlayer(Player ownerPlayer)
        {
            _ownerPlayer = ownerPlayer;
            // это нужно чтобы шмотки знали если что на кого ссылаться
            for (int i = 0; i < items.Count; i++)
            {
                items[i].SetPlayerOwner(_ownerPlayer);
            }

            foreach (var equip in equipped)
            {
                equip.Value?.SetPlayerOwner(_ownerPlayer);
            }
        }

        public List<Item> GetEquippedItems()
        {
            return equipped.Where(item => item.Value != null).Select(item=>item.Value).ToList();
        }

        public void EquipItem(Item item)
        {
            if (item.HasComponent<EquipableComponent>())
            {
                EquipItem(item.GetComponent<EquipableComponent>());
            }
        }

        public void EquipItem(EquipableComponent equipableComponent)
        {
            if(items.Contains(equipableComponent.Owner) == false)
                return;
            
            if (equipped.ContainsKey(equipableComponent.Slot))
            {
                // Надеваем шмотку
                if (equipped[equipableComponent.Slot] == null)
                {
                    // надеваем
                    equipped[equipableComponent.Slot] = equipableComponent.Owner; 
                    Debug.Log($"Одет предмет в слот {equipableComponent.Slot} - [{equipableComponent.Owner.id} {equipableComponent.Owner.name}]");
                    equipableComponent.SetIsEquip(true);
                    
                    // из списка предметов удаляем
                    RemoveItemInner(equipableComponent.Owner);
                    OnEquippedItem?.Invoke(equipableComponent.Slot,equipableComponent.Owner);
                }
                // заменяем
                else
                {
                    // снимаем 
                    var itemPrev = equipped[equipableComponent.Slot];
                    var itemPrevEquipableComponent = itemPrev.GetComponent<EquipableComponent>();
                    itemPrevEquipableComponent.SetIsEquip(false);
                    OnUnEquippedItem?.Invoke(itemPrevEquipableComponent.Slot);
                    
                    // Удаляем из списка предметов который одеваем одеваем
                    RemoveItemInner(equipableComponent.Owner);
                   
                    // добавляем предмет который сняли
                    AddItemInner(itemPrev);
                    
                    // надеваем
                    equipped[equipableComponent.Slot] = equipableComponent.Owner;
                    equipableComponent.SetIsEquip(true);
                    OnEquippedItem?.Invoke(equipableComponent.Slot,equipableComponent.Owner);
                    Debug.Log($"Одет предмет в слот {equipableComponent.Slot} - [{equipableComponent.Owner.id} {equipableComponent.Owner.name}]");
                }
                OnInventoryChanged?.Invoke();
            }
        }

        public void UnEquipItem(Item item)
        {
            if (item.HasComponent<EquipableComponent>())
            {
                UnEquipItem(item.GetComponent<EquipableComponent>());
            }
        }
        
        public void UnEquipItem(EquipableComponent equipableComponent)
        {
            if (equipped.ContainsKey(equipableComponent.Slot))
            {
                if (equipped[equipableComponent.Slot] != null)
                {
                    equipped[equipableComponent.Slot] = null;
                    equipableComponent.SetIsEquip(false);
                    Debug.Log($"Снят предмет из слота {equipableComponent.Slot} - [{equipableComponent.Owner.id} {equipableComponent.Owner.name}]");
                    AddItemInner(equipableComponent.Owner);
                    OnUnEquippedItem?.Invoke(equipableComponent.Slot);
                    OnInventoryChanged?.Invoke();
                }
            }
        }
        
        public void AddItem(Item item)
        {
            item.SetPlayerOwner(_ownerPlayer);
            AddItemInner(item);
            OnInventoryChanged?.Invoke();
        }
        
        private void AddItemInner(Item item)
        {
            item.OnItemUpdated += ItemUpdated;
            item.OnRequestToDestroy += OnRequestToDestroy;
            items.Add(item);
            OnItemAdded?.Invoke(item);
            Debug.Log($"Добавлен предмет в инвентарь - [{item.id} {item.name}]");
        }

        private void ItemUpdated(Item item)
        {
            OnItemUpdated?.Invoke(item);
        }

        public void RemoveItem(Item item)
        {
            RemoveItemInner(item);
            OnInventoryChanged?.Invoke();
        }
        
        private void RemoveItemInner(Item item)
        {
            item.OnItemUpdated -= ItemUpdated;
            item.OnRequestToDestroy -= OnRequestToDestroy;
            items.Remove(item);
            OnItemRemoved?.Invoke(item);
            Debug.Log($"Удален предмет из инвентаря - [{item.id} {item.name}]");
        }

        private void OnRequestToDestroy(Item item)
        {
            RemoveItem(item);
        }

        public void RemoveAll()
        {
            items.Clear();
            OnInventoryChanged?.Invoke();
        }
    }
}
