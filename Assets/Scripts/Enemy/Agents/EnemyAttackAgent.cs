using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        [SerializeField] private WeaponComponent weaponComponent;
        [SerializeField] private EnemyMoveAgent moveAgent;

        //public delegate void FireHandler(GameObject enemy, Vector2 position, Vector2 direction);
        public delegate void FireHandler(GameObject enemy);
        public event FireHandler OnFire;
        
        private GameObject _target;

        public void SetTarget(GameObject target)
        {
            _target = target;
        }

        private void FixedUpdate()
        {
            if (!moveAgent.IsReached)
            {
                return;
            }
            
            if (!_target.GetComponent<HitPointsComponent>().IsHitPointsExists())
            {
                return;
            }
            Fire();
        }

        private void Fire()
        {
            var startPosition = weaponComponent.Position;
            var vector = _target.transform.position - (Vector3)startPosition;
            var direction = vector.normalized;
            
            Debug.DrawLine(transform.position, transform.position + direction,Color.red);
            moveAgent.SetDirectLook(direction);
            OnFire?.Invoke(gameObject);
        }
    }
}