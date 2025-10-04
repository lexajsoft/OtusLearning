using System;
using System.Collections.Generic;

public class DynamicResource
{
    private Dictionary<Stats, int> _currentCharacteristics;
    public Stats StatMain { get; internal set; }
    public Stats StatRegen { get; internal set; }

    public int CurrentValue { get; private set; }
    public int MaxValue { get; private set; }
    public int RegenValue { get; private set; }

    public Action OnValueUpdated;
    
    public virtual void SetCurrentCharacteristics(Dictionary<Stats, int> currentCharacteristics)
    {
        _currentCharacteristics = currentCharacteristics;
    }

    public virtual void Reset()
    {
        CurrentValue = MaxValue = _currentCharacteristics.TryGetValue(StatMain, out var mainValue) ? mainValue : 0;
        RegenValue = _currentCharacteristics.TryGetValue(StatRegen, out var regenValue) ? regenValue : 0;
        OnValueUpdated?.Invoke();
    }

    public virtual void UpdatedCurrentCharacteristics()
    {
        MaxValue = _currentCharacteristics.TryGetValue(StatMain, out var mainValue) ? mainValue : 0;
        RegenValue = _currentCharacteristics.TryGetValue(StatRegen, out var regenValue) ? regenValue : 0;
        OnValueUpdated?.Invoke();
    }

    public virtual void Update()
    {
        CurrentValue += RegenValue;
        if (CurrentValue > MaxValue)
        {
            CurrentValue = MaxValue;
        }
        OnValueUpdated?.Invoke();
    }

    public virtual void AddResources(int value)
    {
        CurrentValue += value;
        if (CurrentValue > MaxValue)
        {
            CurrentValue = MaxValue;
        }
        OnValueUpdated?.Invoke();
    }
    
    public virtual void MinusResources(int value)
    {
        CurrentValue -= value;
        if (CurrentValue < 0)
        {
            CurrentValue = 0;
        }
        OnValueUpdated?.Invoke();
    }

}