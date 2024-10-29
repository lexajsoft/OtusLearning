using UniRx;
using UnityEngine;

namespace Code
{
    public interface IProductPresenter : IPresenter
    {
        string Title { get; }
        string Description { get; }
        Sprite Icon { get; }
        string Price { get; }
        IReadOnlyReactiveProperty<bool> CanBuy { get; }
        ReactiveCommand BuyCommand { get; }
        void Buy();
    }
}
