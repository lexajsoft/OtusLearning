using Inventory.Components;
using UnityEngine;

namespace Inventory.VisualDecorator
{
    public abstract class DecoratorBase<T> : MonoBehaviour where T : ItemComponent
    {
        protected T _itemComponent;
        protected GameObject _container;
        public void SetContainer(GameObject container)
        {
            _container = container;
        }

        public void SetData(T itemComponent)
        {
            _itemComponent = itemComponent;
        }

        public abstract void UpdateVisual();
    }
}