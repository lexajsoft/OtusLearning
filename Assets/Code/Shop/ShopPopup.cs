using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public sealed class ShopPopup : MonoBehaviour
    {
        [SerializeField] private Transform _container; 
        [SerializeField] private ProductView _viewPrefab;
        [SerializeField] private Button _hideButton;
    
        private readonly List<ProductView> _views = new();
    
        public void Show(IPresenter args)
        {
            if (args is not IShopPopupPresenter shopPopupPresenter)
            {
                throw new InvalidOperationException("Expected ShopPopupPresenter");
            }
            
            gameObject.SetActive(true);

            for (var index = 0; index < shopPopupPresenter.ProductPresenters.Count; index++)
            {
                IProductPresenter productPresenter = shopPopupPresenter.ProductPresenters[index];
                ProductView productView = Instantiate(_viewPrefab, _container);
                productView.Initialized(productPresenter);
                _views.Add(productView);
            }
            
            _hideButton.onClick.AddListener(Hide);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
            for (var i = _views.Count - 1; i >= 0; i--)
            {
                ProductView productView = _views[i];
                Destroy(productView.gameObject);
            }
            
            _views.Clear();
            _hideButton.onClick.RemoveListener(Hide);
        }
    }
}
