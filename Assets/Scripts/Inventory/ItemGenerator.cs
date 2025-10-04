using System;
using System.Linq;
using System.Numerics;
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
                    CreateDurability(ref item, levelRare);
                    break;
                }
                case EquipSlot.Chest:
                {
                    item.SetName("Грудь");
                    CreateProperties(ref item, levelRare);
                    CreateDurability(ref item, levelRare);
                    item.SetIcon(_iconConfig.Chests.GetRandom());
                    
                    break;
                }
                case EquipSlot.Arms:
                {
                    item.SetName("Руки");
                    CreateProperties(ref item, levelRare);
                    CreateDurability(ref item, levelRare);
                    item.SetIcon(_iconConfig.Arms.GetRandom());
                    break;
                }
                case EquipSlot.Feet:
                {
                    item.SetName("Ботинки");
                    CreateProperties(ref item, levelRare);
                    CreateDurability(ref item, levelRare);
                    item.SetIcon(_iconConfig.Feets.GetRandom());
                    break;
                }
                case EquipSlot.Weapon:
                {
                    item.SetName("Топор");
                    CreateProperties(ref item, levelRare);
                    CreateDurability(ref item, levelRare);
                    CreateWeaponProperties(ref item, levelRare);
                    item.SetIcon(_iconConfig.Weapons.GetRandom());
                    break;
                }
            }

            return item;
        }

        private void CreateProperties(ref Item item, LevelRare levelRare)
        {
            // Выдача рандомных параметров
            var propertiesComponent = item.AddComponent<StatsComponent>();
            var values = Enum.GetValues(typeof(Stats)).Cast<Stats>().ToList();

            int countProperties = (int) levelRare;
            for (int i = 0; i < countProperties; i++)
            {
                if (values.Count() == 1)
                    break;

                int index = Random.Range(1, values.Count());
                propertiesComponent.properties.Add(new Stat()
                {
                    stat = values[index],
                    Value = Random.Range(1, 20)
                });
                values.RemoveAt(index);
            }
        }

        public void CreateDurability(ref Item item, LevelRare levelRare)
        {
            // Выдача прочности
            int durability = 10 + ((int) levelRare * (int) levelRare * 10);
            item.AddComponent(new DurabilityComponent(durability,durability));
        }

        private void CreateWeaponProperties(ref Item item, LevelRare levelRare)
        {
            float cooldown = Random.Range(2, 3f);
            int damageMin = Random.Range(2 + 2 * (int) levelRare, 4 + 4 * (int) levelRare);
            int damageMax = Random.Range(5 + 2 * (int) levelRare, 8 + 4 * (int) levelRare);
            var damage = new UnityEngine.Vector2(damageMin, damageMax);

            var weaponComponent = new WeaponComponent(damage, cooldown);
            // Выдача рандомных параметров
            item.AddComponent(weaponComponent);
        }

        public Item CreateEtc()
        {
            Item item = new Item();
            item.SetLevelRare(LevelRare.Normal);
            item.SetName("Какая то жратва");
            item.SetIcon(_iconConfig.Items.GetRandom());
            item.AddComponent(new StackableComponent(Random.Range(0,10),10));
            item.AddComponent<UseAbleComponent>();
            return item;
        }
        
        public Item CreateTalisman()
        {
            Item item = new Item();
            item.SetLevelRare(LevelRare.Rare);
            item.SetName("Талисман");
            item.SetIcon(_iconConfig.Items.GetRandom());
            item.AddComponent<DefaultAlwaysEquippedComponent>();
            CreateProperties(ref item, LevelRare.Rare);
            return item;
        }
    }
}