using System;

namespace ShootEmUp
{
    public interface IInputManager
    {
        event Action OnFire;
        event Action<int> OnHorizontalMove;
    }
}