using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.Components;

namespace Inventory.Observers
{
    // Реагирует если хоть как то поменялся инвентарь и если он поменялся то делает перерасчет предметов
    // в инвентаре и тех что одеты, собирает все статы и передает игроку 
    public class EquipItemObserver : IDisposable
    {
        private readonly Player _player;

        public EquipItemObserver(Player player)
        {
            _player = player;
            _player.inventory.OnInventoryChanged += OnInventoryChanged;
            OnInventoryChanged();
        }

        public void Dispose()
        {
            if(_player != null && _player.inventory != null)
                _player.inventory.OnInventoryChanged -= OnInventoryChanged;
        }

        private void OnInventoryChanged()
        {
            List<StatsComponent> items = new List<StatsComponent>();
            var itemsInInventory = _player.inventory
                .items
                .Where(
                    item => item.HasComponent<DefaultAlwaysEquippedComponent>() 
                            && item.HasComponent<StatsComponent>() 
                        )
                .Select(item => item.GetComponent<StatsComponent>())
                .ToList();
            
            var itemsEquipped = _player.inventory
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
            _player.RebuildCharacteristics(statsLst);
        }
    }
}