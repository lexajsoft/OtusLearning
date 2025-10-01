using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace ShootEmUp
{
    public sealed class InputManager : ITickable
    {
        public float HorizontalDirection { get; private set; }

        public UnityEvent OnFire { get; } = new UnityEvent();
        public UnityEvent<int> OnHorizontalMove { get; } = new UnityEvent<int>();

        public void Tick()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                OnFire?.Invoke();
            }

            HorizontalDirection = Input.GetAxis("Horizontal");
            OnHorizontalMove?.Invoke((int)HorizontalDirection);
        }
    }
}