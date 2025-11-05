using System;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace ShootEmUp
{
    public sealed class InputManager :  IInitializable,ITickable
    {
        public float HorizontalDirection { get; private set; }

        public Action OnFire;
        public Action<int> OnHorizontalMove;

        public void Tick()
        {
            //Debug.Log("Tick");
            if (Input.GetKey(KeyCode.Space))
            {
                OnFire?.Invoke();
            }

            HorizontalDirection = Input.GetAxis("Horizontal");
            OnHorizontalMove?.Invoke((int)HorizontalDirection);
        }

        public void Initialize()
        {
            
        }
    }
}