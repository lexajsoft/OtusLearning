using Inventory.Components;
using TMPro;
using UnityEngine;

namespace Inventory.VisualDecorator
{
    public class StackableDecorator : DecoratorBase<StackableComponent>
    {
        [SerializeField] private TextMeshProUGUI _valueText;
        public override void UpdateVisual()
        {
            _valueText.text = $"{_itemComponent.currentCount}";
        }
    }
}