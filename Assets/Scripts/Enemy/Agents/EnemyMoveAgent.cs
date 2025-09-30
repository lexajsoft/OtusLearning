using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        [SerializeField] private MoveComponent moveComponent;
        
        public bool IsReached { get; private set; }
        private Vector2 destination;

        public void SetDestination(Vector2 endPoint)
        {
            destination = endPoint;
            IsReached = false;
        }

        public void SetDirectLook(Vector2 direct)
        {
            moveComponent.SetDirectLook(direct);
        }

        private void Update()
        {
            if (IsReached)
            {
                moveComponent.SetDirectMove(Vector2.zero);
                return;
            }
            
            var vector = destination - (Vector2) transform.position;
            if (vector.magnitude <= 0.25f)
            {
                IsReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            moveComponent.SetDirectMove(direction);
            moveComponent.SetDirectLook(direction);
        }
    }
}