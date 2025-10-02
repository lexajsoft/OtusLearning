using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Inventory.Components
{
    public interface IWeapon
    {
        int GetDamage();
    }

    [Serializable]
    public class WeaponComponent : ItemComponent, IWeapon
    {
        [field: SerializeField] public Vector2 minMaxDamage { get; private set; } = new Vector2(1, 1);
        [field: SerializeField] public float cooldown { get; private set; }
        
        public int GetDamage()
        {
            return (int) Random.Range(minMaxDamage.x, minMaxDamage.y);
        }
    }
}