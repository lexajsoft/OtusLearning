using Inventory.Components;
using UnityEngine;
using UnityEngine.Serialization;

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
            Add(item, _durabilityDecorator);
            Add(item, _equipableDecorator);
            Add(item, _useableDecorator);
            Add(item, _stackableDecorator);
            Add(item, _stackableDecorator);
            
            
            // if (item.HasComponent<DurabilityComponent>())
            // {
            //     var obj = Instantiate(_durabilityDecorator, _container.transform);
            //     obj.SetContainer(_container);
            //     obj.SetData(item.GetComponent<DurabilityComponent>());
            //     obj.UpdateVisual();
            // }
            //
            // if (item.HasComponent<EquipableComponent>())
            // {
            //     var obj = Instantiate(_equipableDecorator, _container.transform);
            //     obj.SetContainer(_container);
            //     obj.SetData(item.GetComponent<EquipableComponent>());
            //     obj.UpdateVisual();
            // }
            //
            // if (item.HasComponent<UseAbleComponent>())
            // {
            //     var obj = Instantiate(_useableDecorator, _container.transform);
            //     obj.SetContainer(_container);
            //     obj.SetData(item.GetComponent<UseAbleComponent>());
            //     obj.UpdateVisual();
            // }
            //
            // if (item.HasComponent<StackableComponent>())
            // {
            //     var obj = Instantiate(_stackableDecorator, _container.transform);
            //     obj.SetContainer(_container);
            //     obj.SetData(item.GetComponent<StackableComponent>());
            //     obj.UpdateVisual();
            // }
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