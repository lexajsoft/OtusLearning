using UnityEngine;

namespace HomeWorkTask2
{
    public class Task2 : MonoBehaviour
    {
        [SerializeField] private ChestMonoManager _chestMonoManager;
        private ChestsDataRepository _chestsDataRepository;
        
        private void Start()
        {
            _chestMonoManager.Init();
            
            _chestsDataRepository = new ChestsDataRepository();
            _chestsDataRepository.OnLoaded += ChestsDataRepositoryOnOnLoaded;
            _chestMonoManager.OnRequestSave += ChestMonoManagerOnOnRequestSave;
            _chestsDataRepository.Load();
            
        }

        private void ChestMonoManagerOnOnRequestSave()
        {
            _chestsDataRepository.Save();
        }

        private void ChestsDataRepositoryOnOnLoaded()
        {
            _chestMonoManager.SetChestsData(_chestsDataRepository.GetData());
        }
    }
}