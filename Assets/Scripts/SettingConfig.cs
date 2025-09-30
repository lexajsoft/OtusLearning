using ShootEmUp;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameConfigInstaller", menuName = "Installers/GameConfigInstaller")]
public class SettingConfig : ScriptableObjectInstaller<SettingConfig>
{
    public BulletConfig _playerBulletConfig;
    public BulletConfig _enemyBulletConfig;
    
    public override void InstallBindings()
    {
        Container.BindInstance(this).AsSingle();
    }
}