using UnityEngine;

namespace ShootEmUp
{
    public class HorizontalInputHandler : InputHandler<HorizontalDirectData>
    {
        private float _direct = 0;
        public override void HandleInput()
        {
            var newDirection = Input.GetAxis("Horizontal");
        
            if (!Mathf.Approximately(_direct, newDirection))
            {
                _direct = newDirection;
                OnInputPerformed(new HorizontalDirectData((int)newDirection));
            }
        }
    }
}