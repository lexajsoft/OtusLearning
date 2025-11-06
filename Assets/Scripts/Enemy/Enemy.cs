using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class Enemy : Npc
    {
        [SerializeField] private EnemyMoveAgent _enemyMoveAgent;
        [SerializeField] private EnemyAttackAgent _enemyAttackAgent;

        public EnemyAttackAgent EnemyAttackAgent => _enemyAttackAgent;
        public EnemyMoveAgent EnemyMoveAgent => _enemyMoveAgent;

        [Inject]
        public void Construct(BulletConfig config)
        {
            WeaponComponent.SetConfig(config);
        }
    
        public class Factory : PlaceholderFactory<BulletConfig,Enemy>
        {
        
        }
    }
}