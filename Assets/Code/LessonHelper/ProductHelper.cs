using UnityEngine;
using Zenject;

namespace Code
{
    public sealed class ProductHelper : MonoBehaviour
    {
        [SerializeField] private ProductInfo _productInfo;
        [SerializeField] private ProductPopup _productPopup;
        [SerializeField] private ShopPopup _shopPopup;
        [SerializeField] private ProductCatalog _productCatalog;
        private ProductPresenterFactory _productPresenterFactory;
        
        [Inject]
        private void Construct(ProductPresenterFactory productPresenterFactory)
        {
            _productPresenterFactory = productPresenterFactory;
        }
        
        public void ProductPopupShow()
        {
            ProductPresenter productPresenter = _productPresenterFactory.Create(_productInfo);
            _productPopup.Show(productPresenter);
        }
        
        public void ShopPopupShow()
        {
           _shopPopup.Show(new ShopPopupPresenter(_productCatalog, _productPresenterFactory));
        }
    }
}
