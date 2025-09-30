using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyPool _enemyPool;
        
        private readonly HashSet<GameObject> m_activeEnemies = new();

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                var enemy = _enemyPool.SpawnEnemy();
                if (enemy != null)
                {
                    if (m_activeEnemies.Add(enemy))
                    {
                        enemy.GetComponent<HitPointsComponent>().hpEmpty += this.OnDestroyed;
                        enemy.GetComponent<EnemyAttackAgent>().OnFire += this.OnFire;
                    }    
                }
            }
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (m_activeEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().hpEmpty -= this.OnDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnFire -= this.OnFire;

                _enemyPool.UnSpawnEnemy(enemy);
            }
        }

        private void OnFire(GameObject enemy)
        {
            enemy.GetComponent<WeaponComponent>().Fire();
            // _bulletSystem.CreateBulletByArgs(new BulletSystem.Args
            // {
            //     isPlayer = false,
            //     color = Color.red,
            //     damage = 1,
            //     position = position,
            //     velocity = direction * 2.0f
            // });
        }
    }
}