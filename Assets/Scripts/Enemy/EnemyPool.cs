using System.Collections.Generic;
using UnityEngine;
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

        [SerializeField] private int _prepareEnemyCount = 10;
        [Inject] private GameManager _gameManager;
        [Inject] private Enemy.Factory _enemyFactory;
        [Inject(Id = ServiceIds.EnemyBulletConfig)] private BulletConfig _bulletConfig;

        private readonly Queue<Enemy> _enemyPool = new();
        private GameObject _character;


        private void Start()
        {
            
            _gameManager.OnCharacterChanged += OnCharacterChanged;
            if(_gameManager.Character != null)
                OnCharacterChanged(_gameManager.Character);
            
            for (var i = 0; i < _prepareEnemyCount; i++)
            {
                var enemy = _enemyFactory.Create(_bulletConfig);
                enemy.transform.SetParent(_container);
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
            foreach (var obj in _enemyPool) obj.GetComponent<EnemyAttackAgent>().SetTarget(_character);
        }

        public Enemy SpawnEnemy()
        {
            if (_character == null)
                return null;

            if (!_enemyPool.TryDequeue(out var enemy)) return null;

            enemy.transform.SetParent(_worldTransform);

            var spawnPosition = _enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;

            var attackPosition = _enemyPositions.RandomAttackPosition();
            enemy.EnemyMoveAgent.SetDestination(attackPosition.position);
            enemy.EnemyAttackAgent.SetTarget(_character);
            return enemy;
        }

        public void UnSpawnEnemy(Enemy enemy)
        {
            enemy.transform.SetParent(_container);
            _enemyPool.Enqueue(enemy);
        }
    }
}