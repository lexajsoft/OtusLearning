using UnityEngine;

namespace GamePlay
{
    public interface IEntity
    {
        Vector3 GetPosition();
        string Type { get; }
        string ID { get; }
        string Name { get; }
        IEntityData Data { get; }
        void SetData(IEntityData entityData);
        GameObject GetGameObject();
    }
}

