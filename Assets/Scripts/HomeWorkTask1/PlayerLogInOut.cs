using System;
using System.Collections;
using UnityEngine;

namespace HomeWorkTask1
{
    public class PlayerLogInOut : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI _timeInGame;
        [SerializeField] private TMPro.TextMeshProUGUI _timeLogIn;
        [SerializeField] private TMPro.TextMeshProUGUI _timeLogOut;

        private string DefaultValueToPrint => "...";
        private LogInGameData _data;

        public void SetData(LogInGameData data)
        {
            _data = data;
            _data.OnUpdated += UpdateData;
            UpdateData();
        }

        private void UpdateData()
        {
            if(_data == null)
                return;
        
            if (_data.IsComplete)
            {
                _timeLogIn.text = DateFormat(_data.LogIn);
                _timeLogOut.text = DateFormat(_data.LogOut);
                _timeInGame.text = SecondsToFormat(_data.SecondsInGame);

            }
            else
            {
                _timeLogIn.text = DateFormat(_data.LogIn);
                _timeLogOut.text = DefaultValueToPrint;
            
                var secondsInGame  = _data.GetTimeDifferenceSeconds(DateTime.UtcNow);
                _timeInGame.text = SecondsToFormat(secondsInGame);
            }
        }

        private void OnEnable()
        {
            _timeLogIn.text = DefaultValueToPrint;
            _timeLogOut.text = DefaultValueToPrint;
            _timeInGame.text = SecondsToFormat(0);
        
            StartCoroutine(SecondTick());
        }

        private IEnumerator SecondTick()
        {
            yield return new WaitWhile(()=>_data == null);
            while (true)
            {
                UpdateData();
                if (_data.IsComplete)
                {
                    yield break;
                }
                yield return new WaitForSeconds(1);
            }
        }

        private string DateFormat(DateTime dateTime)
        {
            return dateTime.ToString("MM/dd/yyyy HH:mm:ss");
        }

        private string SecondsToFormat(long seconds)
        {
            // variant #1
            var timeSpan = TimeSpan.FromSeconds(seconds);
            return $"{timeSpan.TotalHours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
        
            // variant #2
            return $"{(seconds / 3600):00}:{(seconds / 60 % 60):00}:{(seconds % 60):00}";
        }
    }
}
