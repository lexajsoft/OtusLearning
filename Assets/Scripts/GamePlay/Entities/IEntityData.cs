using UnityEngine;

namespace GamePlay
{
    public interface IEntityData
    {
        string GetId();
        void SetId(string id);
        string GetName();
        void SetName(string name);
        string GetUnitDataType();
        void SetUnitDataType(string unitDataType);
        Vector3 GetPosition();
        void SetPosition(Vector3 position);
        
        Quaternion GetRotation();
        void SetRotation(Quaternion rotatino);
        
        Vector3 GetScale();
        void SetScale(Vector3 scale);
    }
}