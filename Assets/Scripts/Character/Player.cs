using Zenject;

namespace ShootEmUp
{
    public class Player : Npc
    {
        [Inject]
        public void Construct(BulletConfig config)
        {
            WeaponComponent.SetConfig(config);
        }

        public class Factory : PlaceholderFactory<BulletConfig, Player>
        {

        }
    }
}