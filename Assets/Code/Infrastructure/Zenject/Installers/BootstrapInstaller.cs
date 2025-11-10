using Code.Gameplay.Signals;
using Code.Infrastructure.Services.EventBus;
using Code.Infrastructure.Services.Input;
using Code.UI;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Zenject.Installers
{
  public class BootstrapInstaller : MonoInstaller
  {
    override public void InstallBindings()
    {
      Debug.Log("ProjectContext Install");
      BindInputService();
      BindSignalBus();
    }

    private void BindInputService()
    {
      Container.Bind<IInputService>().To<InputService>().AsSingle();
    }

    private void BindSignalBus()
    {
      SignalBusInstaller.Install(Container);
      Container.Bind<IEventBus>().To<ZenjectEventBus>().AsSingle();
      Container.DeclareSignal<UnitSelectSignal>();
      Container.DeclareSignal<UnitUnselectSignal>();
      Container.DeclareSignal<AttackEnemySignal>();
    }
  }
}