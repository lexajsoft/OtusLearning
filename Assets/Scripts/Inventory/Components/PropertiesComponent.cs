using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Components
{
    [Serializable]
    public class PropertiesComponent : ItemComponent
    {
        [field: SerializeField] public List<Property> properties { get; private set; }
        public PropertiesComponent()
        {
            properties = new List<Property>();
        }
        public PropertiesComponent(List<Property> properties)
        {
            this.properties = properties;
        }
    }
}