using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public sealed class ProductPopup : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _title;

        [SerializeField]
        private TMP_Text _description;

        [SerializeField]
        private Image _icon;

        [SerializeField]
        private BuyButton _buyButton;
        
        [SerializeField] private Button _closeButton;
        private IProductPresenter _presenter;
        private readonly CompositeDisposable _disposable = new ();

        public void Show(IPresenter args)
        {
            if (args is not IProductPresenter presenter)
            {
                throw new InvalidOperationException("Expected ProductPresenter");
            }
            
            _presenter = presenter;
            
            gameObject.SetActive(true);

            _presenter.BuyCommand.BindTo(_buyButton.Button).AddTo(_disposable);
            _presenter.CanBuy.Subscribe(OnCanBuy).AddTo(_disposable);
            _buyButton.SetPrice(presenter.Price);
            //_buyButton.AddListener(OnBuyButtonClick);
            
            _title.text = presenter.Title;
            _description.text = presenter.Description;
            _icon.sprite = presenter.Icon;
            
            _closeButton.onClick.AddListener(Hide);
        }

        private void OnCanBuy(bool canBuy)
        {
            BuyButtonState buttonState = canBuy ? BuyButtonState.Available : BuyButtonState.Locked;
            _buyButton.SetState(buttonState);
        }

        private void OnBuyButtonClick()
        {
            if (_presenter.CanBuy.Value)
            {
                _presenter.Buy();
            }
        }

        private void Hide()
        {
            gameObject.SetActive(false);
            _closeButton.onClick.RemoveListener(Hide);
            _buyButton.RemoveListener(OnBuyButtonClick);
            _disposable.Clear();
        }
    }
}
