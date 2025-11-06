using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace HomeWorkTask2
{
    [Serializable]
    public enum ChestType
    {
        ChestWood,
        ChestSilver,
        ChestGold,
    }

    [Serializable]
    public class Chest
    {
        public string NameChest = "";
        public ChestRandomItems chestRandomItems;
        public float DelayTime = 1f; // 1 sec
        public Timer Timer;
        public ChestType ChestType = ChestType.ChestWood;

        public event Action OnTimerStarted;

        public bool IsCanOpen()
        {
            return Timer.IsComplete(ServerTime.GetCurrentTime());
        }

        public bool TryOpen(out List<Item> rewards)
        {
            if (IsCanOpen())
            {
                rewards = chestRandomItems.GetRandomItems();
                Timer.SetEndDateTime(ServerTime.GetCurrentTime() + TimeSpan.FromSeconds(DelayTime));
                OnTimerStarted?.Invoke();
                return true;
            }

            rewards = new List<Item>();
            return false;
        }
    }
}