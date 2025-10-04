using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory
{
    [Serializable]
    public partial class Item
    {
        private static int NextID = 0;
        [NonSerialized] private Player _player = null;

        [field: SerializeField] public int id { get; private set; }
        [field: SerializeField] public string name { get; private set; }
        [field: SerializeField] public int cost { get; private set; }
        [field: SerializeField] public  List<ItemComponent> _components{ get; private set; } = new List<ItemComponent>();
        [field: SerializeField] public  Sprite icon{ get; private set; }
        [field: SerializeField] public  LevelRare levelRare{ get; private set; } = LevelRare.Default;

        public Action<Item> OnItemUpdated { get; set; }
        public Action<Item> OnRequestToDestroy{ get; set; }

        public Player GetOwnerPlayer()
        {
            Debug.Log("GetOwnerPlayer:" + _player.ToString());
            return _player;
        }

        public void SetPlayerOwner(Player player)
        {
            _player = player;
        }

        public Item()
        {
            id = NextID++;
        }

        public void SetIcon(Sprite sprite)
        {
            icon = sprite;
        }

        public void SetLevelRare(LevelRare levelRare)
        {
            this.levelRare = levelRare;
        }

        public Item(string name)
        {
            id = NextID++;
            this.name = name;
        }

        public Item(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
        public Item(int id, string name, int cost)
        {
            this.id = id;
            this.name = name;
            this.cost = cost;
        }
    
    
        // Основной метод для получения компонентов
        public T GetComponent<T>() where T : class, IItemComponent
        {
            return _components.OfType<T>().FirstOrDefault();
        }
    
        // Получение всех компонентов типа
        public List<T> GetComponents<T>() where T : class, IItemComponent
        {
            return _components.OfType<T>().ToList();
        }
    
        // Добавление компонента
        public T AddComponent<T>() where T : ItemComponent, new()
        {
            var component = new T();
            component.Initialize(this);
            _components.Add(component);
            return component;
        }
    
        // Добавление существующего компонента
        public void AddComponent(ItemComponent component)
        {
            component.Initialize(this);
            _components.Add(component);
        }
    
        // Удаление компонента
        public bool RemoveComponent<T>() where T : class, IItemComponent
        {
            var component = _components.OfType<T>().FirstOrDefault() as ItemComponent;
            if (component != null)
            {
                return _components.Remove(component);
            }
            return false;
        }
    
        // Проверка наличия компонента
        public bool HasComponent<T>() where T : class, IItemComponent
        {
            return _components.OfType<T>().Any();
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void RequestToDestroy()
        {
            OnRequestToDestroy?.Invoke(this);
        }

        public void ItemUpdated()
        {
            OnItemUpdated?.Invoke(this);
        }
    }

    
}