using ShootEmUp;
using UnityEngine;
using Zenject;

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
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Bullet _bulletPrefab;
     private GameManager _gameManager;
    
    public override void InstallBindings()
    {
        Debug.Log("InstallBindings : SettingConfig");
        BulletConfigsInstall();
        BulletFactoryInstall();
        GameManagerInstall();
        FactoriesInstall();
    }

    private void GameManagerInstall()
    {
        Container.Bind<GameManager>().AsSingle();
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

        Container.BindFactory<BulletConfig, Enemy, Enemy.Factory>()
            .FromComponentInNewPrefab(_enemyPrefab)
            .WithGameObjectName("Enemy");
    }

    private void BulletConfigsInstall()
    {
        Container.Bind<BulletConfig>().WithId(ServiceIds.PlayerBulletConfig).FromInstance(_playerBulletConfig);
        Container.Bind<BulletConfig>().WithId(ServiceIds.EnemyBulletConfig).FromInstance(_enemyBulletConfig);
    }
}