using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.Components;
using UnityEngine;
using Zenject;

namespace Inventory
{
    public class EquipItemObserver : MonoBehaviour
    {
        [SerializeField] private PlayerInventory _playerInventory;
        private Player _player;

        private void OnEnable()
        {
            _playerInventory.OnPlayerChanged += OnPlayerChanged;
            if (_playerInventory.Player != null)
            {
                OnInventoryChanged();
            }
        }
        private void OnDisable()
        {
            if (_playerInventory.Player != null)
            {
                _playerInventory.OnPlayerChanged -= OnPlayerChanged;
            }
        }

        private void OnPlayerChanged(Player player)
        {
            if(_player != null)
                _player.inventory.OnInventoryChanged -= OnInventoryChanged;
            
            _player = player;

            _player.inventory.OnInventoryChanged += OnInventoryChanged;
            OnInventoryChanged();
        }

        private void OnInventoryChanged()
        {
            List<StatsComponent> items = new List<StatsComponent>();
            var itemsInInventory = _playerInventory
                .Inventory
                .items
                .Where(
                    item => item.HasComponent<DefaultAlwaysEquippedComponent>() 
                            && item.HasComponent<StatsComponent>() 
                        )
                .Select(item => item.GetComponent<StatsComponent>())
                .ToList();
            
            var itemsEquipped = _playerInventory
                .Inventory
                .equipped
                .Where(item => item.Value != null)
                .Select(
                    item => item.Value.HasComponent<EquipableComponent>() 
                            && item.Value.GetComponent<EquipableComponent>().IsEquiped 
                            && item.Value.HasComponent<StatsComponent>() 
                        ? item.Value.GetComponent<StatsComponent>() : null).ToList();
            
            items.AddRange(itemsInInventory);
            items.AddRange(itemsEquipped);
            
            
            List<Stat> statsLst = new List<Stat>();
            for (int i = 0; i < items.Count(); i++)
            {
                for (int j = 0; j < items[i].properties.Count(); j++)
                {
                    statsLst.Add(items[i].properties[j]);
                }
            }
            _playerInventory.Player.RebuildCharacteristics(statsLst);
        }


        
        private void OnItemAdded(Item obj)
        {
            // запросить все предметы которые одеваются
            // отсортировать предметы которые одеты
            // забрать у них статы и сложить
            // сбросить статы у игрока
            // сложить базовые статы игрока и статы предметов
            var statsItemList = _playerInventory.Inventory.items
                .Select(
                    item => item.HasComponent<EquipableComponent>() 
                            && item.GetComponent<EquipableComponent>().IsEquiped 
                            && item.HasComponent<StatsComponent>() 
                        ? item.GetComponent<StatsComponent>() : null).ToList();
            
            List<Stat> statsLst = new List<Stat>();
            for (int i = 0; i < statsItemList.Count(); i++)
            {
                for (int j = 0; j < statsItemList[i].properties.Count(); j++)
                {
                    statsLst.Add(statsItemList[i].properties[j]);
                }
            }
            _playerInventory.Player.RebuildCharacteristics(statsLst);
        }
        
        private void OnItemRemove(Item obj)
        {
            var statsItemList = _playerInventory.Inventory.items.Select(item => item.HasComponent<EquipableComponent>() && item.GetComponent<EquipableComponent>().IsEquiped && item.HasComponent<StatsComponent>() ? item.GetComponent<StatsComponent>() : null).ToList();
            List<Stat> statsLst = new List<Stat>();
            for (int i = 0; i < statsItemList.Count(); i++)
            {
                for (int j = 0; j < statsItemList[i].properties.Count(); j++)
                {
                    statsLst.Add(statsItemList[i].properties[j]);
                }
            }
            _playerInventory.Player.RebuildCharacteristics(statsLst);
        }
    }
}