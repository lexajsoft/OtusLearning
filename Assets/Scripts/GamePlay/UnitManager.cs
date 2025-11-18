using System;
using Code.Extensions;
using Extension;
using GamePlay.Buildings;
using GamePlay.Entities.Units;
using Saver;
using TriInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;

namespace GamePlay
{
    public class UnitManager  : MonoBehaviour, ISavingHandler
    {
        [SerializeField] private EntityCollection _entities = new EntityCollection();
        [SerializeField] private GameObject _worldContainer;
        [Inject] private UnitFactory _unitFactory;
        [Inject] private ISaving _savingService;
        
        
        public string SaveKey => "Units";
        public Type SaveType => typeof(EntityCollection);
        
        private void Start()
        {
            _entities = new EntityCollection();
            _savingService.Register(this);
        }

        private void OnDestroy()
        {
            _savingService.UnRegister(this);
        }


        public object GetValue()
        {
            return _entities;
        }

        public void SetObjectValue(object value)
        {
            _worldContainer.transform.DestroyChildren();
            
            _entities = new EntityCollection();
            // Пересоздание всех юнитов
            EntityCollection result = (EntityCollection) value;
            foreach (var item in result.entities)
            {
                for (int i = 0; i < item.Value.Count; i++)
                {
                    var unit = _unitFactory.Create(item.Key, item.Value[i].ToString());
                    unit.GetGameObject().transform.SetParent(_worldContainer.transform);
                    _entities.Add(unit.Data);
                }
            }
        }


        [Button]
        public void DestoryAll()
        {
            _worldContainer.transform.DestroyChildren();
            _entities = new EntityCollection();
        }
        [Button]
        public void CreateWorker()
        {
            var entity = _unitFactory.Create(typeof(WorkerEntityData).ConvertTypeToKeyName());
            entity.GetGameObject().transform.position =
                new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            entity.GetGameObject().transform.rotation =
                Quaternion.Euler(new Vector3(0, Random.Range(0, 360),0));
            entity.GetGameObject().transform.SetParent(_worldContainer.transform);
            
            _entities.Add(entity.Data);
        }
        
        
        [Button]
        public void CreateKnight()
        {
            var entity = _unitFactory.Create(typeof(KnightEntityData).ConvertTypeToKeyName());
            entity.GetGameObject().transform.position =
                new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            entity.GetGameObject().transform.rotation =
                Quaternion.Euler(new Vector3(0, Random.Range(0, 360),0));
            entity.GetGameObject().transform.SetParent(_worldContainer.transform);
            
            _entities.Add(entity.Data);
        }
        
        [Button]
        public void CreateHouse()
        {
            var entity = _unitFactory.Create(typeof(HouseEntityData).ConvertTypeToKeyName());
            entity.GetGameObject().transform.position =
                new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            entity.GetGameObject().transform.rotation =
                Quaternion.Euler(new Vector3(0, Random.Range(0, 360),0));
            entity.GetGameObject().transform.SetParent(_worldContainer.transform);
            
            _entities.Add(entity.Data);
        }
    }
}