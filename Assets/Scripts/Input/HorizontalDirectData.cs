using System;

namespace ShootEmUp
{
    [Serializable]
    public class HorizontalDirectData : EventData
    {
        public int Direction { get; private set; }
        
        public HorizontalDirectData(int direction)
        {
            Direction = direction;
        }
    }
}