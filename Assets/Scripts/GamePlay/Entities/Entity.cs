using System;
using Extension;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay
{
    public abstract class Entity<T> : MonoBehaviour, IEntity where T : IEntityData
    {
        public T EntityData;
        public string Type => GetType().ConvertTypeToKeyName();
        public string ID => EntityData.GetId();
        public string Name => EntityData.GetName();
        public IEntityData Data => EntityData;
        public  void SetData(IEntityData entityData)
        {
            EntityData = entityData.Convert<T>();
            transform.position = EntityData.GetPosition();
            transform.rotation = EntityData.GetRotation();
            transform.localScale = EntityData.GetScale();
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public Vector3 GetPosition()
        {
            return EntityData.GetPosition();
        }

        private void Update()
        {
            EntityData.SetPosition(transform.position);
            EntityData.SetRotation(transform.rotation);
            EntityData.SetScale(transform.localScale);
        }
    }
}