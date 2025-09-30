using ShootEmUp;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private BulletSystem _bulletSystem;

    public override void InstallBindings()
    {
        Container.Bind<InputManager>().FromInstance(_inputManager);
        Container.Bind<BulletSystem>().FromInstance(_bulletSystem);
    }
}
