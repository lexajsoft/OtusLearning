using UnityEngine;
using UnityEngine.Events;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour
    {
        public float HorizontalDirection { get; private set; }

        public UnityEvent OnFire { get; } = new UnityEvent();
        public UnityEvent<int> OnHorizontalMove { get; } = new UnityEvent<int>();
        
        private void Update()
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