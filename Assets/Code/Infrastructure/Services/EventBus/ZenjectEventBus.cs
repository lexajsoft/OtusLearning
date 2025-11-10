using System;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Services.EventBus
{
  public class ZenjectEventBus : IEventBus
  {
    private SignalBus _signalBus;

    public ZenjectEventBus(SignalBus signalBus)
    {
      _signalBus = signalBus;
      Debug.Log("ZenjectEventBus Instantiated");
    }

    public void RaiseEvent<T>(T evt)
    {
      _signalBus.Fire(evt);
    }

    public void Subscribe<T>(Action<T> handler)
    {
      _signalBus.Subscribe(handler);
    }

    public void Unsubscribe<T>(Action<T> handler)
    {
      _signalBus.TryUnsubscribe(handler);
    }
  }
}