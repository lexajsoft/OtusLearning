using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private SpriteRenderer spriteRenderer;
        public event Action<Bullet, Collider2D> OnTriggerEntered;
        [NonSerialized] public bool isPlayer;
        [NonSerialized] public int damage;

        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnTriggerEntered?.Invoke(this, collision);
        }

        public void SetVelocity(Vector2 velocity)
        {
            rigidbody2D.linearVelocity = velocity;
        }

        public void SetPhysicsLayer(int physicsLayer)
        {
            gameObject.layer = physicsLayer;
        }
        
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }
    }
}