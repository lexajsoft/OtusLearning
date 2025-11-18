using UnityEngine;

namespace GamePlay
{
    [CreateAssetMenu(menuName = "Create UnitsFactory", fileName = "UnitsFactory", order = 0)]
    public class UnitsFactoryInstaller : Zenject.ScriptableObjectInstaller<UnitsFactoryInstaller>
    {
        public override void InstallBindings()
        {
            var unitFactory = Container.Instantiate<UnitFactory>();
            unitFactory.Init();
            Container.Bind<UnitFactory>().FromInstance(unitFactory).AsSingle();
        }
    }
}