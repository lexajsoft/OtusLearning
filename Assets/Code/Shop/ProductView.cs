using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public sealed class ProductView : MonoBehaviour
    {
        [SerializeField] private BuyButton _button;
    
        [SerializeField] private TextMeshProUGUI _titleText;

        [SerializeField] private TextMeshProUGUI _descriptionText;

        [SerializeField] private Image _iconImage;
        private IProductPresenter _productPresenter;
        private CompositeDisposable _disposable = new ();

        public void Initialized(IProductPresenter productPresenter)
        {
            _productPresenter = productPresenter;

            _titleText.text = _productPresenter.Title;
            _descriptionText.text = _productPresenter.Description;
            _iconImage.sprite = _productPresenter.Icon;
            _button.SetPrice(_productPresenter.Price);
            
            _disposable?.Dispose();
            _disposable = new CompositeDisposable();
            _productPresenter.BuyCommand.BindTo(_button.Button).AddTo(_disposable);
            _productPresenter.CanBuy.Subscribe(OnCanBuy).AddTo(_disposable);
        }

        private void OnCanBuy(bool canBuy)
        {
            BuyButtonState buttonState = canBuy ? BuyButtonState.Available : BuyButtonState.Locked;
            _button.SetState(buttonState);
        }
    }
}
