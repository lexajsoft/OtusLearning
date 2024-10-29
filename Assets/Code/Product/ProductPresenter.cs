using UniRx;
using UnityEngine;

namespace Code
{
    public sealed class ProductPresenter : IProductPresenter
    {
        public string Title { get; }
        public string Description { get; }
        public Sprite Icon { get; }
        public string Price { get; }
        public ReactiveCommand BuyCommand { get; }
        public IReadOnlyReactiveProperty<bool> CanBuy => _canBuy;

        private readonly ReactiveProperty<bool> _canBuy = new();

        private readonly ProductInfo _productInfo;
        private readonly ProductBuyer _productBuyer;
        private readonly CompositeDisposable _compositeDisposable = new();

        public ProductPresenter(ProductInfo productInfo, ProductBuyer productBuyer, MoneyStorage moneyStorage)
        {
            _productInfo = productInfo;
            _productBuyer = productBuyer;
            Title = _productInfo.Title;
            Description = _productInfo.Description;
            Icon = _productInfo.Icon;
            Price = _productInfo.MoneyPrice.ToString();

            moneyStorage.Money.Subscribe(OnMoneyChange).AddTo(_compositeDisposable);
            BuyCommand = new ReactiveCommand(CanBuy);
            BuyCommand.Subscribe(OnBuyCommand).AddTo(_compositeDisposable);
            BuyCommand.Pairwise().Subscribe(OnNext).AddTo(_compositeDisposable);
        }

        private void OnNext(Pair<Unit> obj)
        {
        }

        private void OnBuyCommand(Unit _)
        {
            Buy();
        }

        private void OnMoneyChange(long _)
        {
            _canBuy.Value = _productBuyer.CanBuy(_productInfo);
        }

        public void Buy()
        {
            _productBuyer.Buy(_productInfo);
        }

        ~ProductPresenter()
        {
            _compositeDisposable.Dispose();
        }
    }
}
