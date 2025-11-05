using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [CreateAssetMenu(menuName = "Installers/" + nameof(InputManagerInstaller))]
    public class InputManagerInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();
        }
    }
}