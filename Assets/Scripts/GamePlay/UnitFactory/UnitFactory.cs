using System;
using System.Collections.Generic;
using Extension;
using GamePlay.Buildings;
using GamePlay.Entities.Units;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Object = UnityEngine.Object;

namespace GamePlay
{
    public class UnitFactory
    {
        [Inject] private UnitsFactoryConfig _unitsFactoryConfig;
        
        [Serializable]
        public class UnitFactoryData
        {
            public Func<EntityData, IEntity> Factory;
            public Type Type;
            [FormerlySerializedAs("UnitData")] public IEntityData entityData;
            public GameObject Prefab;
        }

        private Dictionary<string, UnitFactoryData> _unitFactories = new();

        public void Init()
        {
            Registry<WorkerEntity, WorkerEntityData>(typeof(WorkerEntityData).ConvertTypeToKeyName());
            Registry<KnightEntity, KnightEntityData>(typeof(KnightEntityData).ConvertTypeToKeyName());
            Registry<HouseEntity, HouseEntityData>(typeof(HouseEntityData).ConvertTypeToKeyName());
            // сюда можно добавлять других юнитов которых захочется указывая их класс и тим данных
        }

        private void Registry<TUnit, TData>(string keyUnitData)
            where TUnit : MonoBehaviour, IEntity, new()
            where TData : IEntityData, new()
        {
            
            var unitFactoryData = new UnitFactoryData();
            unitFactoryData.Prefab = _unitsFactoryConfig.Get(keyUnitData).GetPrefab();
            unitFactoryData.Factory = (data) =>
            {
                var key = data.GetUnitDataType();
                var prefab = _unitFactories[key].Prefab;
                if (prefab != null)
                {
                    var gameObject = Object.Instantiate(prefab);
                    gameObject.name = $"{key}_{data.Id}";
                    var unit = gameObject.AddComponent<TUnit>();
                    unit.SetData(data);
                    return unit;   
                }
                else
                {
                    var gameObject = new GameObject($"{key}_{data.Id}");
                    var unit = gameObject.AddComponent<TUnit>();
                    unit.SetData(data);
                    return unit;    
                }
            };
            unitFactoryData.Type = typeof(TData);
            unitFactoryData.entityData = _unitsFactoryConfig.Get(keyUnitData).GetUnitData();

            _unitFactories[keyUnitData] = unitFactoryData;
        }

        public IEntity Create(string keyUnitData, string jsonData)
        {
            EntityData entityData = (EntityData)JsonConvert.DeserializeObject(jsonData, _unitFactories[keyUnitData].Type);
            return Create(entityData);
        }
        
        public IEntity Create(string keyUnitData)
        {
            EntityData entityData = new EntityData();
            entityData = (EntityData) _unitFactories[keyUnitData].entityData;
            entityData.Id = Guid.NewGuid().ToString();
            return Create(entityData);
        }
        
        public EntityData GetTemplate(string keyUnitData)
        {
            return (EntityData) _unitFactories[keyUnitData].entityData;
        }

        public IEntity Create(EntityData entityData)
        {
            if (_unitFactories.TryGetValue(entityData.UnitDataType, out var value))
            {
                return value.Factory.Invoke(entityData);
            }

            return null;
        }
    }
}