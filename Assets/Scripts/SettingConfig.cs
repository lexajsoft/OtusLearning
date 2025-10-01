using ShootEmUp;
using UnityEngine;
using Zenject;

public static class ServiceIds
{
    public const string PlayerBulletConfig = nameof(PlayerBulletConfig);
    public const string EnemyBulletConfig = nameof(EnemyBulletConfig);
    public const string PlayerPrefab = nameof(PlayerPrefab);
    public const string EnemyPrefab = nameof(EnemyPrefab);
    public const string BulletPrefab = nameof(BulletPrefab);
}

[CreateAssetMenu(fileName = "GameConfigInstaller", menuName = "Installers/GameConfigInstaller")]
public class SettingConfig : ScriptableObjectInstaller<SettingConfig>
{
    [SerializeField] private BulletConfig _playerBulletConfig;
    [SerializeField] private BulletConfig _enemyBulletConfig;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _bulletPrefab;
     private GameManager _gameManager;
    
    public override void InstallBindings()
    {
        Debug.Log("InstallBindings : SettingConfig");
        Container.Bind<BulletConfig>().WithId(ServiceIds.PlayerBulletConfig).FromInstance(_playerBulletConfig);
        Container.Bind<BulletConfig>().WithId(ServiceIds.EnemyBulletConfig).FromInstance(_enemyBulletConfig);
        Container.Bind<GameObject>().WithId(ServiceIds.PlayerPrefab).FromInstance(_playerPrefab);
        Container.Bind<GameObject>().WithId(ServiceIds.EnemyPrefab).FromInstance(_enemyPrefab);
        Container.Bind<GameObject>().WithId(ServiceIds.BulletPrefab).FromInstance(_bulletPrefab);
        Container.Bind<GameManager>().AsSingle();
    }
}