namespace Installers
{
    public class PlayerInstaller : Zenject.Installer<PlayerInstaller>
    {
        public override void InstallBindings()
        {
            var player = Container.Instantiate<Player>();
            Container.Bind<Player>().FromInstance(player);
        }
    }
}