using Inventory.Components;
using UnityEngine;

namespace Inventory.VisualDecorator
{
    public class EquipableDecorator : DecoratorBase<EquipableComponent>
    {
        [SerializeField] private GameObject _isEquiped;
        public override void UpdateVisual()
        {
            _isEquiped.SetActive(_itemComponent.IsEquiped);
        }
    }
}