using UnityEngine;
using Zenject;

namespace SomeService
{
    [CreateAssetMenu(menuName = "Create AudioServiceInstaller", fileName = "AudioServiceInstaller", order = 0)]
    public class AudioServiceInstaller : ScriptableObjectInstaller<AudioServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<AudioService>().FromComponentInHierarchy().AsSingle();
        }
    }
}