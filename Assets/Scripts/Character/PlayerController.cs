using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class PlayerController : MonoBehaviour
    {
        //[SerializeField] private GameObject _character;
        [SerializeField] private GameObject _container;
        [SerializeField] private GameObject _position;
        
        [Inject(Id = ServiceIds.PlayerBulletConfig)] private BulletConfig _bulletConfig;
        [Inject] private IInputManager _inputManager;
        [Inject] private IGameManager _gameManager;
        [Inject] private Player.Factory _factory;
        
        private MoveComponent _moveComponent;
        private WeaponComponent _weapon;
        private bool _fireRequired;
        private Player _player;
        
        private void OnEnable()
        {
            CreatePlayer();
            _gameManager.SetCharacterGameObject(_player);
            
            _player.HitPointsComponent.hpEmpty+= OnPlayerDeath;
            
            _inputManager.OnFire += Fire;
            _inputManager.OnHorizontalMove += HorizontalMove;
        }

        private void CreatePlayer()
        {
            _player = _factory.Create(_bulletConfig);
            _player.transform.SetParent(_container.transform);
            _player.transform.position = _position.transform.position;
        }

        private void HorizontalMove(int horizontalValue)
        {
            if(_player != null)
                _player.MoveComponent.SetDirectMove(new Vector2(horizontalValue, 0) * Time.fixedDeltaTime);
        }

        private void Fire()
        {
            _fireRequired = true;
        }

        private void OnDisable()
        {
            _inputManager.OnFire += Fire;
            _inputManager.OnHorizontalMove += HorizontalMove;
            if (_player != null)
            {
                _player.HitPointsComponent.hpEmpty -= OnPlayerDeath;
            }
        }

        private void OnPlayerDeath(GameObject _)
        {
            _gameManager.FinishGame();
        }

        private void Update()
        {
            if (_fireRequired)
            {
                _player?.WeaponComponent.Fire();
                _fireRequired = false;
            }
        }
    }
}