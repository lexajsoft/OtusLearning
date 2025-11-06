using UnityEngine;
using UnityEngine.UI;

namespace HomeWorkTask1
{
    public class Task1 : MonoBehaviour
    {
        [SerializeField] private PlayerTimeStatisticsRepository _statisticsRepository;
        [SerializeField] private PlayerLogInOutManager _playerLogInOutManager;
        [SerializeField] private Button _logInButton;
        [SerializeField] private Button _logOutButton;
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _loadButton;

        public void Start()
        {
            _statisticsRepository = new PlayerTimeStatisticsRepository();
            _statisticsRepository.OnLoaded += StatisticsRepositoryOnOnLoaded;
            _statisticsRepository.Load();
        
            _logInButton.onClick.AddListener(LogInButton);
            _logOutButton.onClick.AddListener(LogOutButton);
            _saveButton.onClick.AddListener(Save);
            _loadButton.onClick.AddListener(Load);
        }

        private void StatisticsRepositoryOnOnLoaded()
        {
            _playerLogInOutManager.SetPlayerTimeStatisticsRepository(_statisticsRepository);
        }

        private void Load()
        {
            _statisticsRepository.Load();
        }

        private void Save()
        {
            _statisticsRepository.Save();
        }

        private void LogOutButton()
        {
            _statisticsRepository.GetData().RecordTimeLogOut();
        }

        private void LogInButton()
        {
            _statisticsRepository.GetData().RecordTimeLogIn();
        }
    }
}
