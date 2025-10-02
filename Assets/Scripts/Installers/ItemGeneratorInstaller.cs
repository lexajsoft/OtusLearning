using Inventory;

namespace Installers
{
    public class ItemGeneratorInstaller : Zenject.Installer<ItemGeneratorInstaller>
    {
        public override void InstallBindings()
        {
            var itemGenerator = Container.Instantiate<ItemGenerator>();
            Container.Bind<ItemGenerator>().FromInstance(itemGenerator).AsSingle();
        }
    }
}