using System;

namespace HomeWorkTask2
{
    [Serializable]
    public class Timer
    {
        public DateTime _endDateTime;
        public bool IsComplete(DateTime currentDateTime) => currentDateTime >= _endDateTime;
        public double GetRemainTimeSeconds(DateTime currentDateTime)
        {
            if (IsComplete(currentDateTime))
            {
                return 0;
            }
            else
            {
                return (_endDateTime - currentDateTime).TotalSeconds;
            }
        }
        
        public void SetEndDateTime(DateTime dateTime)
        {
            _endDateTime = dateTime;
        }

    }
}