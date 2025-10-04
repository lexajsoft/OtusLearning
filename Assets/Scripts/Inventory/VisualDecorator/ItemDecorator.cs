using Inventory.Components;
using UnityEngine;

namespace Inventory.VisualDecorator
{
    public class ItemDecorator : MonoBehaviour
    {
        [SerializeField] private GameObject _container;
        [SerializeField] private DurabilityDecorator _durabilityDecorator;
        [SerializeField] private UseableDecorator _useableDecorator;
        [SerializeField] private StackableDecorator _stackableDecorator;
        [SerializeField] private EquipableDecorator _equipableDecorator;

        public void SetItem(Item item)
        {
            if (item == null)
                return;
            Add(item, _durabilityDecorator);
            Add(item, _equipableDecorator);
            Add(item, _useableDecorator);
            Add(item, _stackableDecorator);
            
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
    }
}