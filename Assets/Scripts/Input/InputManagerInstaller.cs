using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [CreateAssetMenu(menuName = "Installers/" + nameof(InputManagerInstaller))]
    public class InputManagerInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {

            Container.Bind<InputManager>()
                .AsSingle()
                .NonLazy();

            Container.Bind<IInputManager>()
                .To<InputManager>()
                .FromResolve();

            Container.Bind<IInitializable>()
                .To<InputManager>()
                .FromResolve();

            Container.Bind<ITickable>()
                .To<InputManager>()
                .FromResolve();

            Container.Bind<IDisposable>()
                .To<InputManager>()
                .FromResolve();
        }
    }
}