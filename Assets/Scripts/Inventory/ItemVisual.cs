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
        [Inject] private LevelRareColorConfig _levelRareColorConfig;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _background;
        [SerializeField] private TMPro.TextMeshProUGUI _nameText;
        public UnityAction<Item> OnChanged; 
        [SerializeField]private Item _item;
        [SerializeField] private ItemDecorator _itemDecorator;
        public void SetItem(Item item)
        {
            _item = item;
            OnChanged?.Invoke(_item);
            _itemDecorator.SetItem(_item);
                
            UpdateVisual();
        }

        protected virtual void UpdateVisual()
        {
            _background.color = _levelRareColorConfig.GetColorByLevelRare(_item.levelRare);
            _icon.sprite = _item.icon;
            _nameText.text = _item.name;

        }

    }
}