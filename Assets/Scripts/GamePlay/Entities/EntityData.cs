using System;
using UnityEngine;

namespace GamePlay
{
    [Serializable]
    public class EntityData : IEntityData
    {
        public string Id = "";
        public string Name = "";
        public string UnitDataType = "";
        public MyVector3 Position = new MyVector3(0, 0, 0);
        public MyQuaternion Rotation;
        public MyVector3 Scale = new MyVector3(1, 1, 1);

        public string GetId()
        {
            return Id;
        }

        public void SetId(string id)
        {
            id = id;
        }

        public string GetName()
        {
            return Name;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public string GetUnitDataType()
        {
            return UnitDataType;
        }

        public void SetUnitDataType(string unitDataType)
        {
            UnitDataType = unitDataType;
        }

        public Vector3 GetPosition()
        {
            return Position.GetVector3();
        }

        public void SetPosition(Vector3 vector3)
        {
            Position.Set(vector3);
        }

        public Quaternion GetRotation()
        {
            return Rotation.GetQuaternion();
        }

        public void SetRotation(Quaternion rotation)
        {
            Rotation.Set(rotation);
        }

        public Vector3 GetScale()
        {
            return Scale.GetVector3();
        }

        public void SetScale(Vector3 scale)
        {
            Scale.Set(scale);
        }
    }
}