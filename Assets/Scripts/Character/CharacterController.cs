using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Zenject;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject character; 
        
        // Сериализацию оставил чтобы чекать в инспекторе и не более по факту они тут не нужны
        [Inject][SerializeField] private InputManager _inputManager;
        [Inject][SerializeField] private SettingConfig _settingConfig;
        [Inject][SerializeField] private GameManager gameManager;
        
        private MoveComponent _moveComponent;
        private WeaponComponent _weapon;
        private bool _fireRequired;

        private void OnEnable()
        {
            _inputManager.OnFire.AddListener(Fire);
            _inputManager.OnHorizontalMove.AddListener(HorizontalMove);
            character.GetComponent<HitPointsComponent>().hpEmpty += this.OnCharacterDeath;
            _moveComponent = character.GetComponent<MoveComponent>();
            _weapon = character.GetComponent<WeaponComponent>();
            _weapon.SetConfig(_settingConfig._playerBulletConfig);
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
            character.GetComponent<HitPointsComponent>().hpEmpty -= this.OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject _) => this.gameManager.FinishGame();

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