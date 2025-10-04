using System;
using UnityEngine;

namespace Inventory.Components
{
    [Serializable]
    public class ActivatedComponent : ItemComponent
    {
        [field: SerializeField] public Activation Activation { get; private set; } = Activation.Never;
        public ActivatedComponent()
        {
            
        }

        public void SetActivation(Activation activation)
        {
            Activation = activation;
        }
    }
}