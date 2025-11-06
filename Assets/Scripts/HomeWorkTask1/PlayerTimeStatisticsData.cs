using System;
using System.Collections.Generic;
using UnityEngine;

namespace HomeWorkTask1
{
    public class PlayerTimeStatisticsData
    {
        private LogInGameData _logInGameDataTemp;
        public List<LogInGameData> LogTimes;

        public event Action<LogInGameData> OnNewLogInGameData;
        public event Action<LogInGameData> OnCloseLogInGameData;
    
        public PlayerTimeStatisticsData()
        {
            _logInGameDataTemp = null;
            LogTimes = new List<LogInGameData>();
        }

        public void CheckAfterLoad()
        {
            if (LogTimes.Count > 0)
            {
                if (!LogTimes[LogTimes.Count - 1].IsComplete)
                {
                    _logInGameDataTemp = LogTimes[LogTimes.Count - 1];
                }
            }
        }

        public void RecordTimeLogIn()
        {
            if (_logInGameDataTemp == null)
            {
                _logInGameDataTemp = new LogInGameData();
                _logInGameDataTemp.RecordLogIn();
                LogTimes.Add(_logInGameDataTemp);
                OnNewLogInGameData?.Invoke(_logInGameDataTemp);
                Debug.Log("Зафиксировано время входа");
            
            }
            else
            {
                Debug.LogError("Время входа уже зафиксировано");
            }
        }

        public void RecordTimeLogOut()
        {
            if (_logInGameDataTemp != null)
            {
                _logInGameDataTemp.RecordLogOut();
                OnCloseLogInGameData?.Invoke(_logInGameDataTemp);
                Debug.Log("Зафиксировано время выхода");
                _logInGameDataTemp = null;
            }
            else
            {
                Debug.LogError("Время входа не было запущено");
            }
        }
    }
}