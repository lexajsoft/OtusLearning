using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Timer
{
    public class EventTimer : ITickable, IInitializable
    {
        private float _remain1Sec;
        private float _remain2Sec;
        private float _remain3Sec;
        
        // вызывается каждую секунду
        public UnityAction OnTickEveryOneSecond;
        // вызывается каждую 2ю секунду
        public UnityAction OnTickEveryTwoSecond;
        // вызывается каждую 3ю секунду
        public UnityAction OnTickEveryThreeSecond;
        public UnityAction OnTickEveryFrame;

        public EventTimer()
        {
            
        }

        public void Initialize()
        {
            
        }
        
        public void Tick()
        {
            OnTickEveryFrame?.Invoke();
            UpdateTimer1();
            UpdateTimer2();
            UpdateTimer3();
        }

        private void UpdateTimer1()
        {
            _remain1Sec -= Time.deltaTime;
            if (_remain1Sec <= 0)
            {
                _remain1Sec = 1f;
                OnTickEveryOneSecond?.Invoke();
            }
        }
        
        private void UpdateTimer2()
        {
            _remain2Sec -= Time.deltaTime;
            if (_remain2Sec <= 0)
            {
                _remain2Sec = 2f;
                OnTickEveryTwoSecond?.Invoke();
            }
        }
        
        private void UpdateTimer3()
        {
            _remain3Sec -= Time.deltaTime;
            if (_remain3Sec <= 0)
            {
                _remain3Sec = 2f;
                OnTickEveryThreeSecond?.Invoke();
            }
        }


    }
}