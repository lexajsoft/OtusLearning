using Zenject;

namespace Code
{
    public sealed class ProductInstaller
    {
        public ProductInstaller(DiContainer container)
        {
            container.Bind<ProductBuyer>().AsSingle().NonLazy();
            container.Bind<ProductPresenterFactory>().AsSingle().NonLazy();
        }
    }
}
