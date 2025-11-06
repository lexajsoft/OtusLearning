using System;

namespace ShootEmUp
{
    public abstract class InputHandler<T>  where T : EventData 
    {
        public event EventHandler<T> OnEvent;
        public abstract void HandleInput();
        
        protected virtual void OnInputPerformed(T args)
        {
            OnEvent?.Invoke(this, args);
        }
    }
}