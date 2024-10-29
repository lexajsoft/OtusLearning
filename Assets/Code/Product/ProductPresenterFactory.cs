namespace Code
{
    public sealed class ProductPresenterFactory
    {
        private readonly ProductBuyer _productBuyer;
        private readonly MoneyStorage _moneyStorage;

        public ProductPresenterFactory(ProductBuyer productBuyer, MoneyStorage moneyStorage)
        {
            _productBuyer = productBuyer;
            _moneyStorage = moneyStorage;
        }

        public ProductPresenter Create(ProductInfo productInfo)
        {
            return new ProductPresenter(productInfo, _productBuyer, _moneyStorage);
        }
    }
}
