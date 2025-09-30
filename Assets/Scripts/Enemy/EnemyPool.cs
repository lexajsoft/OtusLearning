using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyPool : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private GameObject _character;
        [SerializeField] private Transform _worldTransform;
        [Header("Pool")]
        [SerializeField] private Transform _container;
        [SerializeField] private GameObject _prefab;
        
        private readonly Queue<GameObject> _enemyPool = new();
        
        [Inject]private SettingConfig _settingConfig;
        [Inject]private DiContainer _diContainer;
        private BulletConfig _bulletConfig;
        
        private void Awake()
        {
            _bulletConfig = _settingConfig._enemyBulletConfig;
            for (var i = 0; i < 7; i++)
            {
                var enemy = _diContainer.InstantiatePrefab(_prefab, _container);
                _enemyPool.Enqueue(enemy);
            }
        }

        public GameObject SpawnEnemy()
        {
            if (!_enemyPool.TryDequeue(out var enemy))
            {
                return null;
            }

            enemy.transform.SetParent(_worldTransform);

            var spawnPosition = _enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;
            
            var attackPosition = _enemyPositions.RandomAttackPosition();
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);

            enemy.GetComponent<EnemyAttackAgent>().SetTarget(_character);
            enemy.GetComponent<WeaponComponent>().SetConfig(_bulletConfig);
            return enemy;
        }

        public void UnSpawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(_container);
            _enemyPool.Enqueue(enemy);
        }
    }
}