using System;
using System.Collections.Generic;
using Extension;
using UnityEngine;

namespace GamePlay
{
    [CreateAssetMenu(menuName = "Create UnitsFactoryConfig", fileName = nameof(UnitsFactoryConfig), order = 0)]
    [Serializable]
    public class UnitsFactoryConfig : ScriptableObject
    {
        [SerializeField] private List<UnitPrefabData> units = new ();
        private Dictionary<string, UnitPrefabData> Presets = new ();
        public void Init()
        {
            Presets = new Dictionary<string, UnitPrefabData>();
            for (int i = 0; i < units.Count; i++)
            {
                var key =  units[i].GetUnitData().GetType().ConvertTypeToKeyName();
                units[i].GetUnitData().SetUnitDataType(key);
                Presets[key] = units[i];
            }
        }

        public UnitPrefabData Get(string keyUnitDataType)
        {
            if (Presets.TryGetValue(keyUnitDataType, out var preset))
            {
                return preset;
            }

            return null;
        }
        
    }
}