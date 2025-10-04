using System;
using System.Collections.Generic;
using Configs;
using Inventory;
using Inventory.Components;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;

public class DynamicResource
{
    private Dictionary<Stats, int> _currentCharacteristics;
    public Stats StatMain { get; internal set; }
    public Stats StatRegen { get; internal set; }

    public int CurrentValue { get; private set; }
    public int MaxValue { get; private set; }
    public int RegenValue { get; private set; }

    public Action OnValueUpdated;
    
    public void SetCurrentCharacteristics(Dictionary<Stats, int> currentCharacteristics)
    {
        _currentCharacteristics = currentCharacteristics;
    }

    public void Reset()
    {
        CurrentValue = MaxValue = _currentCharacteristics.TryGetValue(StatMain, out var mainValue) ? mainValue : 0;
        RegenValue = _currentCharacteristics.TryGetValue(StatRegen, out var regenValue) ? regenValue : 0;
    }

    public void Update()
    {
        CurrentValue += RegenValue;
        if (CurrentValue > MaxValue)
        {
            CurrentValue = MaxValue;
        }
        OnValueUpdated?.Invoke();
    }

    public void AddResources(int value)
    {
        CurrentValue += value;
        if (CurrentValue > MaxValue)
        {
            CurrentValue = MaxValue;
        }
        OnValueUpdated?.Invoke();
    }
    
    public void WasteResources(int value)
    {
        CurrentValue -= value;
        if (CurrentValue < 0)
        {
            CurrentValue = 0;
        }
        OnValueUpdated?.Invoke();
    }

}

[Serializable]
public class Player
{
    public Inventory.Inventory inventory;
    [Inject] private ItemGenerator _itemGenerator;
    [Inject] private DefaultCharacteristicsConfig _defaultCharacteristicsConfig;
    
    public DynamicResource Health { get; private set; }
    public DynamicResource Mana { get; private set; }
    
    public Dictionary<Stats, int> CurrentCharacteristics { get; private set; }
    
    private Dictionary<Stats, int> _defaultCharacteristics;

    public Action OnCurrentCharacteristicsChanged;
    public Player()
    {
        inventory = new Inventory.Inventory();
        _defaultCharacteristics = new Dictionary<Stats, int>();
        CurrentCharacteristics = new Dictionary<Stats, int>();
        Health = new DynamicResource()
        {
            StatMain = Stats.Health,
            StatRegen = Stats.HealthRegen,
        };
        Mana = new DynamicResource()
        {
            StatMain = Stats.Mana,
            StatRegen = Stats.ManaRegen,
        };
    }

    private void UpdateResources()
    {
        Health.Update();
        Mana.Update();
    }

    public void Init()
    {
        AcceptDefaultStats();
        CreateRandomInventory();

        //TimerGlobal.OnEverySecondUpdate += UpdateResources;
        //тут нужен какой ни то таймер который будет дергать каждую секунду Update
    }

    public void AddTestArmor()
    {
        LevelRare levelRare= (LevelRare)Random.Range(0, 5);
        inventory.AddItem(_itemGenerator.CreateItemEquipableItem((EquipSlot)Random.Range(1, 6), levelRare));
    }

    public void AddTalisman()
    {
        inventory.AddItem(_itemGenerator.CreateTalisman());
    }
    
    public void AddEtc()
    {
        inventory.AddItem(_itemGenerator.CreateEtc());
    }

    private void AcceptDefaultStats()
    {
        for (int i = 0; i < _defaultCharacteristicsConfig.Characteristics.Count; i++)
        {
            _defaultCharacteristics[_defaultCharacteristicsConfig.Characteristics[i].stat] =
                _defaultCharacteristicsConfig.Characteristics[i].Value;
        }
    }

    public void CreateRandomInventory()
    {
        LevelRare levelRare = (LevelRare)Random.Range(0, 5);
        inventory.AddItem(_itemGenerator.CreateItemEquipableItem(EquipSlot.Head, levelRare));
        levelRare = (LevelRare)Random.Range(0, 5);
        inventory.AddItem(_itemGenerator.CreateItemEquipableItem(EquipSlot.Arms, levelRare));
        levelRare = (LevelRare)Random.Range(0, 5);
        inventory.AddItem(_itemGenerator.CreateItemEquipableItem(EquipSlot.Chest, levelRare));
        levelRare = (LevelRare)Random.Range(0, 5);
        inventory.AddItem(_itemGenerator.CreateItemEquipableItem(EquipSlot.Feet, levelRare));
        levelRare = (LevelRare)Random.Range(0, 5);
        inventory.AddItem(_itemGenerator.CreateItemEquipableItem(EquipSlot.Weapon, levelRare));


    }

    
    
    public void RebuildCharacteristics(List<Stat> stats)
    {
        CurrentCharacteristics.Clear();
        foreach (var item in _defaultCharacteristics)
        {
            CurrentCharacteristics[item.Key] = item.Value;
        }

        for (int i = 0; i < stats.Count; i++)
        {
            if (CurrentCharacteristics.ContainsKey(stats[i].stat))
            {
                CurrentCharacteristics[stats[i].stat] += stats[i].Value;
            }
            else
            {
                CurrentCharacteristics[stats[i].stat] = stats[i].Value;
            }
        }
        
        OnCurrentCharacteristicsChanged?.Invoke();
    }
}