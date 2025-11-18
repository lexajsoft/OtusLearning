using UnityEngine;

namespace GamePlay
{
    [CreateAssetMenu(menuName = "Create UnitsFactoryConfigInstaller", fileName = nameof(UnitsFactoryConfigInstaller), order = 0)]
    public class UnitsFactoryConfigInstaller : Zenject.ScriptableObjectInstaller<UnitsFactoryConfigInstaller>
    {
        [SerializeReference] private UnitsFactoryConfig _unitsFactoryConfig;
        public override void InstallBindings()
        {
            var config =  ScriptableObject.Instantiate(_unitsFactoryConfig);
            config.Init();            
            
            Container.Bind<UnitsFactoryConfig>().FromInstance(config).AsSingle();
        }
    }
}