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
}