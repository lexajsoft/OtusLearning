using System;

namespace HomeWorkTask1
{
    [Serializable]
    public class LogInGameData
    {
        public event Action OnUpdated;
    
        public DateTime LogIn { get; set; }
        public DateTime LogOut { get; set; }
        public long SecondsInGame  { get; set; } = 0;
        public bool IsComplete { get; set; } = false;

        public LogInGameData()
        {
            IsComplete = false;
            LogIn = DateTime.MinValue;
            LogOut = DateTime.MinValue;
        }
    
        public long GetTimeDifferenceSeconds(DateTime dateTimeNowUTC)
        {
            return (long)(dateTimeNowUTC - LogIn).TotalSeconds;
        }

        public void RecordLogIn()
        {
            if (IsComplete) 
                return;
        
            LogIn = DateTime.UtcNow;
            OnUpdated?.Invoke();
        }
        public void RecordLogOut()
        {
            if (IsComplete) 
                return;
        
            LogOut = DateTime.UtcNow;
            IsComplete = true;
            ReCalculateTimeInGame();
            OnUpdated?.Invoke();
        }

        public void ReCalculateTimeInGame()
        {
            if (IsComplete)
            {
                // значение после запятой откидываем изза ненадобности
                SecondsInGame = (long) (LogOut - LogIn).TotalSeconds;
            }
        }
    }
}