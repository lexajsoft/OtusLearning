using ShootEmUp;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public static class ServiceIds
    {
        public const string PlayerBulletConfig = nameof(PlayerBulletConfig);
        public const string EnemyBulletConfig = nameof(EnemyBulletConfig);
    }

    [CreateAssetMenu(fileName = "GameConfigInstaller", menuName = "Installers/" + nameof(SettingConfigInstaller))]
    public class SettingConfigInstaller : ScriptableObjectInstaller<SettingConfigInstaller>
    {
        [SerializeField] private BulletConfig _playerBulletConfig;
        [SerializeField] private BulletConfig _enemyBulletConfig;
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private ShootEmUp.Enemy _enemyPrefab;
        [SerializeField] private Bullet _bulletPrefab;

        public override void InstallBindings()
        {
            Debug.Log("SettingConfigInstaller.InstallBindings");
            BulletConfigsInstall();
            BulletFactoryInstall();
            GameManagerInstall();
            FactoriesInstall();
        }

        private void GameManagerInstall()
        {
            Container.Bind<IGameManager>().To<GameManager>().AsSingle();
        }

        private void BulletFactoryInstall()
        {
            Container.BindFactory<Bullet, Bullet.Factory>()
                .FromComponentInNewPrefab(_bulletPrefab)
                .WithGameObjectName("Bullet");
        }

        private void FactoriesInstall()
        {
            Container.BindFactory<BulletConfig, Player, Player.Factory>()
                .FromComponentInNewPrefab(_playerPrefab)
                .WithGameObjectName("Player");

            Container.BindFactory<BulletConfig, ShootEmUp.Enemy, ShootEmUp.Enemy.Factory>()
                .FromComponentInNewPrefab(_enemyPrefab)
                .WithGameObjectName("Enemy");
        }

        private void BulletConfigsInstall()
        {
            Container.Bind<BulletConfig>().WithId(ServiceIds.PlayerBulletConfig).FromInstance(_playerBulletConfig);
            Container.Bind<BulletConfig>().WithId(ServiceIds.EnemyBulletConfig).FromInstance(_enemyBulletConfig);
        }
    }
}