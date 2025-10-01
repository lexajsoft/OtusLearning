using System;
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
        [SerializeField] private Transform _worldTransform;
        [Header("Pool")]
        [SerializeField] private Transform _container;
        [SerializeField] private GameObject _prefab;
        
        [Inject]private DiContainer _diContainer;
        [Inject(Id = ServiceIds.EnemyBulletConfig)] private BulletConfig _bulletConfig;
        [Inject] private GameManager _gameManager;
        
        private readonly Queue<GameObject> _enemyPool = new();
        private GameObject _character;
        
        
        private void Awake()
        {
            _gameManager.OnCharacterChanged += OnCharacterChanged;
            for (var i = 0; i < 7; i++)
            {
                var enemy = _diContainer.InstantiatePrefab(_prefab, _container);
                _enemyPool.Enqueue(enemy);
            }
        }

        private void OnDestroy()
        {
            _gameManager.OnCharacterChanged -= OnCharacterChanged;
        }

        private void OnCharacterChanged(GameObject character)
        {
            _character = character;
            foreach (var obj in _enemyPool)
            {
                obj.GetComponent<EnemyAttackAgent>().SetTarget(_character);   
            }
        }

        public GameObject SpawnEnemy()
        {
            if (_character == null)
                return null;
            
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