using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Zenject;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject _character;
        [SerializeField] private GameObject _container;
        [SerializeField] private GameObject _position;
        
        [Inject(Id = ServiceIds.PlayerPrefab)] private GameObject _characterPrefab;
        [Inject(Id = ServiceIds.PlayerBulletConfig)] private BulletConfig _bulletConfig;
        [Inject] private InputManager _inputManager;
        [Inject] private GameManager _gameManager;
        [Inject] private DiContainer _diContainer;
        
        private MoveComponent _moveComponent;
        private WeaponComponent _weapon;
        private bool _fireRequired;

        private void OnEnable()
        {
            CreatePlayer();
            _gameManager.SetCharacterGameObject(_character);
            _inputManager.OnFire.AddListener(Fire);
            _inputManager.OnHorizontalMove.AddListener(HorizontalMove);
            _character.GetComponent<HitPointsComponent>().hpEmpty += this.OnCharacterDeath;
            _moveComponent = _character.GetComponent<MoveComponent>();
            _weapon = _character.GetComponent<WeaponComponent>();
            _weapon.SetConfig(_bulletConfig);
        }

        private void CreatePlayer()
        {
            _character = _diContainer.InstantiatePrefab(_characterPrefab);
            _character.transform.SetParent(_container.transform);
            _character.transform.position = _position.transform.position;
        }

        private void HorizontalMove(int horizontalValue)
        {
            _moveComponent.SetDirectMove(new Vector2(horizontalValue, 0) * Time.fixedDeltaTime);
        }

        private void Fire()
        {
            _fireRequired = true;
        }

        private void OnDisable()
        {
            _inputManager.OnFire.RemoveListener(Fire);
            _inputManager.OnHorizontalMove.RemoveListener(HorizontalMove);
            if (_character.TryGetComponent<HitPointsComponent>(out var hitPointsComponent))
            {
                hitPointsComponent.hpEmpty -= OnCharacterDeath;
            }
        }

        private void OnCharacterDeath(GameObject _)
        {
            _gameManager.FinishGame();
        }

        private void Update()
        {
            if (_fireRequired)
            {
                _weapon.Fire();
                _fireRequired = false;
            }
        }
    }
}