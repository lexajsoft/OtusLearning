using System;

namespace Code.Infrastructure.Services.EventBus
{
  public interface IEventBus
  {
    void RaiseEvent<T>(T evt);
    void Subscribe<T>(Action<T> handler);
    void Unsubscribe<T>(Action<T> handler);
  }
}