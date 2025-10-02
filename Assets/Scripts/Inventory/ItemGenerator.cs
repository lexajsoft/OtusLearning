using System;
using System.Linq;
using Configs;
using Extension;
using Inventory.Components;
using Zenject;
using Random = UnityEngine.Random;

namespace Inventory
{
    public class ItemGenerator
    {
        [Inject] private IconConfig _iconConfig;

        public Item CreateItemEquipableItem(EquipSlot equipSlot, LevelRare levelRare)
        {
            Item item = new Item();
            item.SetLevelRare(levelRare);
            item.AddComponent(new EquipableComponent(equipSlot));
            
            switch (equipSlot)
            {
                case EquipSlot.NONE:
                {
                    return null;
                }
                case EquipSlot.Head:
                {
                    item.SetName("Шлем");
                    item.SetIcon(_iconConfig.Heads.GetRandom());
                    CreateProperties(ref item, levelRare);
                    break;
                }
                case EquipSlot.Chest:
                {
                    item.SetName("Руки");
                    CreateProperties(ref item, levelRare);
                    item.SetIcon(_iconConfig.Chests.GetRandom());
                    break;
                }
                case EquipSlot.Arms:
                {
                    item.SetName("Руки");
                    CreateProperties(ref item, levelRare);
                    item.SetIcon(_iconConfig.Arms.GetRandom());
                    break;
                }
                case EquipSlot.Feet:
                {
                    item.SetName("Ботинки");
                    CreateProperties(ref item, levelRare);
                    item.SetIcon(_iconConfig.Feets.GetRandom());
                    break;
                }
                case EquipSlot.Weapon:
                {
                    item.SetName("Топор");
                    CreateProperties(ref item, levelRare);
                    item.SetIcon(_iconConfig.Weapons.GetRandom());
                    break;
                }
            }

            return item;
        }

        private void CreateProperties(ref Item item, LevelRare levelRare)
        {
            // Выдача рандомных параметров
            var propertiesComponent = item.AddComponent<PropertiesComponent>();
            var values = Enum.GetValues(typeof(Characteristics)).Cast<Characteristics>().ToList();

            int countProperties = (int) levelRare;
            for (int i = 0; i < countProperties; i++)
            {
                if (values.Count() == 1)
                    break;

                int index = Random.Range(1, values.Count());
                propertiesComponent.properties.Add(new Property()
                {
                    Characteristic = values[index],
                    Value = Random.Range(1, 20)
                });
                values.RemoveAt(index);
            }

            // Выдача прочности
            int durability = 10 + ((int) levelRare * (int) levelRare * 10);
            item.AddComponent(new DurabilityComponent(durability,durability));
        }
    }
}