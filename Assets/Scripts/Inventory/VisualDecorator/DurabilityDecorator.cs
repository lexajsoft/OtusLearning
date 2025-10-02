using Inventory.Components;
using TMPro;
using UnityEngine;

namespace Inventory.VisualDecorator
{
    public class DurabilityDecorator : DecoratorBase<DurabilityComponent>
    {
        [SerializeField] private TextMeshProUGUI _valueText;
        public override void UpdateVisual()
        {
            _valueText.text = $"{_itemComponent.CurrentDurability}/{_itemComponent.MaxDurability}";
        }
    }
}