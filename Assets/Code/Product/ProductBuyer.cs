using UnityEngine;

namespace Code
{
    public sealed class ProductBuyer
    {
        private readonly MoneyStorage _moneyStorage;

        public ProductBuyer(MoneyStorage moneyStorage)
        {
            _moneyStorage = moneyStorage;
        }

        public void Buy(ProductInfo product)
        {
            if (CanBuy(product))
            {
                _moneyStorage.SpendMoney(product.MoneyPrice);
                Debug.Log($"<color=green>Product {product.Title} successfully purchased!</color>");
            }
            else
            {
                Debug.LogWarning($"<color=red>Not enough money for product {product.Title}!</color>");
            }
        }

        public bool CanBuy(ProductInfo product)
        {
            return _moneyStorage.Money.Value >= product.MoneyPrice;
        }
    }
}
