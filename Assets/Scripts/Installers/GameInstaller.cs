using ShootEmUp;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private BulletSystem _bulletSystem;

    public override void InstallBindings()
    {
        Debug.Log("InstallBindings : GameInstaller");
        Container.Bind<BulletSystem>().FromInstance(_bulletSystem);
    }
}
