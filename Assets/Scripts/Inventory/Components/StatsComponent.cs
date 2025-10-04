using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Components
{
    [Serializable]
    public class StatsComponent : ItemComponent
    {
        [field: SerializeField] public List<Stat> properties { get; private set; }
        public StatsComponent()
        {
            properties = new List<Stat>();
        }
        public StatsComponent(List<Stat> properties)
        {
            this.properties = properties;
        }
    }
}