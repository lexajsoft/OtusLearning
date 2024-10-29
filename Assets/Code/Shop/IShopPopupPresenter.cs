using System.Collections.Generic;

namespace Code
{
    public interface IShopPopupPresenter : IPresenter
    {
        IReadOnlyList<IProductPresenter> ProductPresenters { get; }
    }
}
