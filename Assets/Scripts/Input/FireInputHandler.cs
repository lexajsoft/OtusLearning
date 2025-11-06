using UnityEngine;

namespace ShootEmUp
{
    public class FireInputHandler : InputHandler<EventData>
    {
        public override void HandleInput()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                OnInputPerformed(EventData.Empty);
            }
        }
    }
}