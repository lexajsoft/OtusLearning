using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        [SerializeField] private int hitPoints;
        public event Action<GameObject> hpEmpty;
        
        
        public bool IsHitPointsExists() 
        {
            return hitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            hitPoints -= damage;
            if (hitPoints <= 0)
            {
                hpEmpty?.Invoke(gameObject);
            }
        }
    }
}