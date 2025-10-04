using System;
using System.Collections.Generic;
using System.Linq;
using Configs;
using Extension;
using Inventory;
using Inventory.Components;
using Inventory.Observers;
using Timer;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

[Serializable]
public class Player : ITickable
{
    public Inventory.Inventory inventory;
    
    [Inject] private ItemGenerator _itemGenerator;
    [Inject] private DefaultCharacteristicsConfig _defaultCharacteristicsConfig;
    [Inject] private EventTimer _eventTimer;
    
    private EquipItemObserver _equipItemObserver;
    
    public DynamicResource Health { get; private set; }
    public DynamicResource Mana { get; private set; }
    
    public Dictionary<Stats, int> CurrentCharacteristics { get; private set; }
    
    private Dictionary<Stats, int> _defaultCharacteristics;

    public Action OnCurrentCharacteristicsChanged { get;  set; }

    public Player()
    {
        inventory = new Inventory.Inventory();
        inventory.SetPlayer(this);
        
        _defaultCharacteristics = new Dictionary<Stats, int>();
        CurrentCharacteristics = new Dictionary<Stats, int>();
        
        // динамичные параметры игрока
        Health = new DynamicResource()
        {
            StatMain = Stats.Health,
            StatRegen = Stats.HealthRegen,
        };
        Health.SetCurrentCharacteristics(CurrentCharacteristics);
        
        Mana = new DynamicResource()
        {
            StatMain = Stats.Mana,
            StatRegen = Stats.ManaRegen,
        };
        Mana.SetCurrentCharacteristics(CurrentCharacteristics);

        OnCurrentCharacteristicsChanged += UpdateValuesResources;
        
        
        _equipItemObserver = new EquipItemObserver(this);
    }
    private void UpdateValuesResources()
    {
        Health.UpdatedCurrentCharacteristics();
        Mana.UpdatedCurrentCharacteristics();
    }

    private void UpdateRegenResources()
    {
        Health.Update();
        Mana.Update();
    }

    public void Init()
    {
        AcceptDefaultStats();
        CreateRandomInventory();

        Health.Reset();
        Mana.Reset();

        // для обновления регенерации
        _eventTimer.OnTickEveryOneSecond += UpdateRegenResources;
        _eventTimer.Tick();
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

    public void Damage(int damage)
    {
        // логика получания урона
        // из брони высчитывается урон и то что остается наносится игроку,
        // если урон поглащен полность то по дефолту наносится урон в 1
        // если урон изначально был никакой то игнор и выход
        
        if(damage <= 0)
            return;

        var armors = inventory.GetEquippedItems();
        var durabilityComponents = armors.Where(item => item.GetComponent<DurabilityComponent>().IsBroken == false).Select(item=>item.GetComponent<DurabilityComponent>()).ToList();
        if (durabilityComponents.Count > 0)
        {
            durabilityComponents.GetRandom().ReduceDurability(1);
        }

        int resultDamage = 0;
        if (CurrentCharacteristics.ContainsKey(Stats.Armor))
        {
            var armor = CurrentCharacteristics[Stats.Armor];
            resultDamage = damage - armor;
            // проверка на то поглотила ли броня урон весь
            // и если да то ставим минимальный урон в 1
            if (resultDamage <= 0)
            {
                resultDamage = 1;
            }
            Health.MinusResources(resultDamage);
        }

         
    }

    public void WastMana(int mana)
    {
        Mana.MinusResources(mana);
    }

    /////////////////////////////////////////////////////////////////////////////////////
    /// TEST
    /////////////////////////////////////////////////////////////////////////////////////
    public void AddTestArmor()
    {
        LevelRare levelRare= (LevelRare)Random.Range(0, 5);
        inventory.AddItem(_itemGenerator.CreateItemEquipableItem((EquipSlot)Random.Range(1, 6), levelRare));
    }

    public void AddTalisman()
    {
        inventory.AddItem(_itemGenerator.CreateTalisman());
    }  
    public void AddBottleHealth()
    {
        LevelRare levelRare= (LevelRare)Random.Range(0, 5);
        inventory.AddItem(_itemGenerator.CreateBottleHeal(levelRare));
    }    
    public void AddBottleMana()
    {
        LevelRare levelRare= (LevelRare)Random.Range(0, 5);
        inventory.AddItem(_itemGenerator.CreateBottleMana(levelRare));
    }    
    public void AddBottleHealthAndMana()
    {
        LevelRare levelRare= (LevelRare)Random.Range(0, 5);
        inventory.AddItem(_itemGenerator.CreateComplexBottle(levelRare));
    }
    
    public void AddEtc()
    {
        inventory.AddItem(_itemGenerator.CreateEtc());
    }

    public void AddRepairKit()
    {
        inventory.AddItem(_itemGenerator.CreateRepairKit());
    }

    public void Tick()
    {
        Debug.Log("Tick");
    }
}