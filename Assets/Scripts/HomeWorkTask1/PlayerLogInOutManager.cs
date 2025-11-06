using UnityEngine;

namespace HomeWorkTask1
{
    public class PlayerLogInOutManager : MonoBehaviour
    {
        [SerializeField] private PlayerLogInOut _playerLogInOutPrefab;
        [SerializeField] private Transform _container;
        private PlayerTimeStatisticsRepository _playerTimeStatisticsRepository;

        public void SetPlayerTimeStatisticsRepository(PlayerTimeStatisticsRepository statisticsRepository)
        {
            DestroyChildren();
            if (_playerTimeStatisticsRepository != null)
            {
                _playerTimeStatisticsRepository.GetData().OnNewLogInGameData -= Create;
            }
            _playerTimeStatisticsRepository = statisticsRepository;
            _playerTimeStatisticsRepository.GetData().OnNewLogInGameData += Create;
            Rebuild();
        }

        private void Rebuild()
        {
            DestroyChildren();

            if (_playerTimeStatisticsRepository != null && _playerTimeStatisticsRepository.GetData().LogTimes.Count > 0)
            {
                var logTimes = _playerTimeStatisticsRepository.GetData().LogTimes;
                for (int i = 0; i < logTimes.Count; i++)
                {
                    Create(logTimes[i]);
                }
            }
        }

        private void Create(LogInGameData logInGameData)
        {
            var playerLogInOut = Instantiate(_playerLogInOutPrefab, _container);
            playerLogInOut.SetData(logInGameData);
        }

        private void DestroyChildren()
        {
            int childCount = _container.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Destroy(_container.GetChild(i).gameObject);
            }
        }
    }
}