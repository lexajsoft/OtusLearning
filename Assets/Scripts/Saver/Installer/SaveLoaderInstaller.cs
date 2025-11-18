using UnityEngine;

namespace Saver.Installer
{
    [CreateAssetMenu(menuName = "Installer/Create " + nameof(SavingService), fileName = nameof(SavingService), order = 0)]
    public class SaveLoaderInstaller : Zenject.ScriptableObjectInstaller<SaveLoaderInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ISaving>().To<SavingService>().AsSingle();
            // загрузка существующего сохранения
            Container.Resolve<ISaving>().Load();
        }
    }
}