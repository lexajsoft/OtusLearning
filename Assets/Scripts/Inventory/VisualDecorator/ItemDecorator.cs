using Extensions;
using Inventory.Components;
using UnityEngine;

namespace Inventory.VisualDecorator
{
    public class ItemDecorator : MonoBehaviour
    {
        [SerializeField] private GameObject _container;
        [SerializeField] private DurabilityDecorator _durabilityDecorator;
        [SerializeField] private UsableDecorator usableDecorator;
        [SerializeField] private StackableDecorator _stackableDecorator;
        [SerializeField] private EquipableDecorator _equipableDecorator;
        private Item _item;

        public void SetItem(Item item)
        {
            _item = item;
            if (item == null)
                return;
        }

        private void Rebuild()
        {
            _container.transform.DestroyAll();
            if (_item == null)
                return;
            
            Add(_item, _durabilityDecorator);
            Add(_item, _equipableDecorator);
            Add(_item, usableDecorator);
            Add(_item, _stackableDecorator);
        }

        private void Add<T>(Item item, DecoratorBase<T> decorator) where T : ItemComponent
        {
            if (item.HasComponent<T>())
            {
                var obj = Instantiate(decorator, _container.transform);
                obj.SetContainer(_container);
                obj.SetData(item.GetComponent<T>());
                obj.UpdateVisual();
            }
        }

        public void UpdateVisual()
        {
            Rebuild();
        }
    }
}