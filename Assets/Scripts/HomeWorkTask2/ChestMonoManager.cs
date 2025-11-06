using System;
using System.Collections.Generic;
using UnityEngine;

namespace HomeWorkTask2
{
    public class ChestMonoManager : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private ChestMono _chestMonoPrefab;
        [SerializeField] private Icons _icons;
        
        private ChestsData _chestsData;

        public event Action OnRequestSave;

        public void Init()
        {
            _icons.Init();
        }

        public void SetChestsData(ChestsData chestsData)
        {
            _chestsData = chestsData;
            Rebuild();
        }

        private void Rebuild()
        {
            var chests = _chestsData.Chests;
            for (int i = 0; i < chests.Count; i++)
            {
                var chest = Instantiate(_chestMonoPrefab, _container);
                chest.SetIcons(_icons);
                chest.SetChest(chests[i]);
                chest.OnTryOpen += OnTryOpen;
            }   
        }

        private void OnTryOpen(Chest chest)
        {
            if (chest.TryOpen(out List<Item> items))
            {
                ShowReward(items);
                // show Reward
                OnRequestSave?.Invoke();
            }
        }

        private void ShowReward(List<Item> rewards)
        {
            string resultItems = "";
            for (int i = 0; i < rewards.Count; i++)
            {
                resultItems += $"{rewards[i].Name} {rewards[i].Count}" + System.Environment.NewLine;
            }
            Debug.Log("Награда из сундука " + Environment.NewLine + resultItems);
        }
    }
}