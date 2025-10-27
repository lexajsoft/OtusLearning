using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        [Inject] [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float _reloadDelay = 0.25f;
        public Vector2 Position => firePoint.position;
        public Quaternion Rotation => firePoint.rotation;

        private BulletConfig _bulletConfig;
        private float _reloadRemainTime;
        private bool _isReady = true;

        public void SetConfig(BulletConfig bulletConfig)
        {
            _bulletConfig = bulletConfig;
        }

        public void Fire()
        {
            if (!_isReady)
                return;

            _bulletSystem.CreateBulletByArgs(new BulletSystem.Args
            {
                isPlayer = _bulletConfig.isPlayer,
                color = _bulletConfig.color,
                damage = _bulletConfig.damage,
                physicsLayer = (int) _bulletConfig.physicsLayer,
                position = Position,
                velocity = firePoint.up * _bulletConfig.speed
            });
            Reload();
        }

        private void Reload()
        {
            _isReady = false;
            _reloadRemainTime = _reloadDelay;
        }

        private void Update()
        {
            if (!_isReady)
            {
                _reloadRemainTime -= Time.deltaTime;
                if (_reloadRemainTime < 0) _isReady = true;
            }
        }
    }
}