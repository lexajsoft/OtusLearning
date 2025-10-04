using System;
using System.Collections.Generic;
using Configs;
using Inventory.VisualDecorator;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Inventory
{
    public class ItemVisual : MonoBehaviour
    {
        [Inject] protected LevelRareColorConfig _levelRareColorConfig;
        
        [SerializeField] private Button _button;
        [SerializeField] protected Image _iconItem;
        [SerializeField] protected TMPro.TextMeshProUGUI _nameText;
        [SerializeField] protected List<Image> _recoloringElements;

        [Space]
        // включен SerializeField только для проверок и не более
        [SerializeField] private bool _isUserDecorator;
        [SerializeField] protected ItemDecorator _itemDecorator;
        [SerializeField] protected Item _item;
        
        public UnityAction<Item> OnChanged;
        public UnityAction<ItemVisual> OnClicked;

        public Item Item => _item;
        
        private void OnEnable()
        {
            _button.onClick.AddListener(Click);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Click);
        }

        private void Click()
        {
            OnClicked?.Invoke(this);
        }

        public void SetItem(Item item)
        {
            _item = item;
            OnChanged?.Invoke(_item);

            if(_itemDecorator != null)
            {
                if (_isUserDecorator)
                {
                    _itemDecorator.SetItem(_item);
                }
                else
                {
                    _itemDecorator.SetItem(null);
                }
            }
                
            UpdateVisual();
        }

        

        protected virtual void UpdateVisual()
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
                _nameText.text = _item.name;
            }
        }

    }
}