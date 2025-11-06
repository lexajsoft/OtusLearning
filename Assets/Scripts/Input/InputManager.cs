using System;
using Zenject;

namespace ShootEmUp
{
    public sealed class InputManager :  IInputManager, IInitializable,ITickable, IDisposable
    {
        public event Action OnFire;
        public event Action<int> OnHorizontalMove;

        private HorizontalInputHandler _horizontalInputHandler;
        private FireInputHandler _fireInputHandler;

        public void Tick()
        {
            _fireInputHandler.HandleInput();
            _horizontalInputHandler.HandleInput();
        }
        
        public void Initialize()
        {
            _horizontalInputHandler = new HorizontalInputHandler();
            _fireInputHandler = new FireInputHandler();

            _horizontalInputHandler.OnEvent += HorizontalMove;
            _fireInputHandler.OnEvent += Fire;
        }

        private void Fire(object sender, EventData e)
        {
            OnFire?.Invoke();
        }

        private void HorizontalMove(object sender, HorizontalDirectData e)
        {
            OnHorizontalMove?.Invoke(e.Direction);
        }

        public void Dispose()
        {
            _horizontalInputHandler.OnEvent -= HorizontalMove;
            _fireInputHandler.OnEvent -= Fire;
        }
    }
}