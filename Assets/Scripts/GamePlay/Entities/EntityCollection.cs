using System;
using System.Collections.Generic;
using System.Linq;
using Extension;
using UnityEngine.Serialization;

namespace GamePlay
{
    [Serializable]
    public class EntityCollection 
    {
        //public List<> units = new List<IUnit>();
        // key = Type
        // Value = List<object>
        public Dictionary<string, List<object>> entities = new Dictionary<string, List<object>>();

        public List<T> Get<T>()
        {
            var key = typeof(T).ConvertTypeToKeyName();
            if (entities.TryGetValue(key, out var result))
            {
                return result.Select(item => (T)item).ToList();
            }

            return new List<T>();
        }

        public void Add(IEntityData entity)
        {
            var key = entity.GetUnitDataType();
            if (!entities.ContainsKey(key))
            {
                entities[key] = new List<object>();
            }
            entities[key].Add(entity);
        }
    }
}