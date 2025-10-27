using Zenject;

namespace ShootEmUp
{
    public class InputManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();
        }
    }
    
    public class InputManagerInstaller2 : Installer<InputManagerInstaller2>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();
        }
    }
}