using Zenject;

namespace Code
{
    public sealed class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            new CurrencyInstaller(Container);
            new ProductInstaller(Container);
        }
    }
}
