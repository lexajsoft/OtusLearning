using System;
using System.Collections.Generic;
using UnityEditor;

namespace Code.Entity
{
    // идея сделать персонажа 
    // у которого хп, урон, способности и все остальное находится в одном месте либо разделить все
    
    public class Character
    {
        private Dictionary<IEntityComponent, Type> 
    }

    public class Entity
    {
        
    }

    public interface IEntityComponent
    {
        public Entity Owner { get; private set; }
    }

    [Serializable]
    public abstract class EntityComponent : IEntityComponent
    {
        public Entity Owner { get; private set; }
    
        public virtual void Initialize(Progress.Item owner)
        {
            Owner = owner;
        }
    }
}