using System;
using Extension;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay
{
    [Serializable]
    public class UnitPrefabData
    {
        [SerializeReference] private IEntityData entityData;
        [SerializeField] private GameObject _prefab;

        public UnitPrefabData() { }

        public UnitPrefabData(IEntityData entityData, GameObject prefab)
        {
            this.entityData = entityData;
            _prefab = prefab;
        }

        public void SetUnitData(IEntityData entityData)
        {
            this.entityData = entityData;
        }
        
        public (string, GameObject) Get()
        {
            return (entityData.GetType().ConvertTypeToKeyName(), _prefab);
        }

        public GameObject GetPrefab()
        {
            return _prefab;
        }

        public IEntityData GetUnitData()
        {
            return entityData;
        }
    }
}