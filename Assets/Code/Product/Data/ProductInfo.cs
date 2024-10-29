using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = "Product", menuName = "Data/New Product")]
    public sealed class ProductInfo : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _title; 
        [SerializeField] [TextArea(3, 5)] private string _description;
        [Space]
        [SerializeField] private int _moneyPrice;

        public Sprite Icon => _icon; 
        public string Title => _title; 
        public string Description => _description;
        public int MoneyPrice => _moneyPrice;
    }
}
