using System;
using System.Collections.Generic;
using Inventory.Components.Effects;
using UnityEngine;

namespace Inventory.Components
{
    [Serializable]
    public class UsableComponent : ItemComponent
    {
        [field: SerializeReference] public List<IEffectAction> Effects { get; private set; } = new List<IEffectAction>();

        public UsableComponent()
        {
        }

        public void AddEffect(IEffectAction effectAction)
        {
            Effects.Add(effectAction);
        }

        public void Use()
        {
            Debug.Log("Использование предмета Началось");
            for (int i = 0; i < Effects.Count; i++)
            {
                Debug.Log($"[i:{i}]Активируется эффект: {Effects[i].GetDescription()}");
                Effects[i].Use(Owner);
            }
            Debug.Log("Использование предмета завершено");
        }
    }
}