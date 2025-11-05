using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private float _spawnDelay = 1f;
        private readonly HashSet<GameObject> _activeEnemies = new();

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnDelay);
                var enemy = _enemyPool.SpawnEnemy();
                if (enemy != null)
                {
                    if (_activeEnemies.Add(enemy.gameObject))
                    {
                        enemy.GetComponent<HitPointsComponent>().hpEmpty += OnDestroyed;
                        enemy.GetComponent<EnemyAttackAgent>().OnFire += OnFire;
                    }    
                }
            }
        }

        private void OnDestroyed(GameObject enemyGameObject)
        {
            if (_activeEnemies.Remove(enemyGameObject))
            {
                if (enemyGameObject.TryGetComponent<Enemy>(out var enemy))
                {
                    enemy.GetComponent<HitPointsComponent>().hpEmpty -= OnDestroyed;
                    enemy.GetComponent<EnemyAttackAgent>().OnFire -= OnFire;
                    _enemyPool.UnSpawnEnemy(enemy);
                }
            }
        }

        private void OnFire(GameObject enemyGameObject)
        {
            if(enemyGameObject.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.WeaponComponent.Fire();
            }
            
        }
    }
}