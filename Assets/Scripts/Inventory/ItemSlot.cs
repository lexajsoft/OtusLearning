using System;
using Inventory.Components;
using UnityEngine;

namespace Inventory
{
    public class ItemSlot : ItemVisual
    {
        [SerializeField] private EquipSlot _equipSlot;

        [SerializeField] private Sprite _defaultIcon;
        [SerializeField] private string _nameSlot;
        [SerializeField] private Color _defaultColor= Color.gray;

        public EquipSlot EquipSlot => _equipSlot;
        protected override void UpdateVisual()
        {
            if (_item != null)
            {
                var color = _levelRareColorConfig.GetColorByLevelRare(_item.levelRare);

                for (int i = 0; i < _recoloringElements.Count; i++)
                {
                    _recoloringElements[i].color = color;
                }

                _nameText.color = color;
                _iconItem.sprite = _item.icon;
                _nameText.text = String.IsNullOrEmpty(_nameSlot) ? EquipSlot.ToString(): _nameSlot;
            }
            else
            {
                for (int i = 0; i < _recoloringElements.Count; i++)
                {
                    _recoloringElements[i].color = _defaultColor;
                }
                _iconItem.sprite = _defaultIcon;
                _nameText.text = String.IsNullOrEmpty(_nameSlot) ? EquipSlot.ToString(): _nameSlot;
                _nameText.color = _defaultColor;
            }
        }
    }
}
