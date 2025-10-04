using System.Linq;
using UnityEngine;
using Zenject;

namespace Timer
{
    public class EventTimerInstaller : Installer<EventTimerInstaller>
    {
        public override void InstallBindings()
        {
            var timer = new EventTimer();
            
            Container.BindInterfacesAndSelfTo<EventTimer>().FromInstance(timer).AsSingle();
            
            // !!! это какой то бред, я не понимаю почему при инсталяции через monoInstaller оно начинает работать,
            // а через обычный Installer идет на отказ добавлять его в TickableManager
            Container.Resolve<TickableManager>().Add(timer);;
        }
    }
}