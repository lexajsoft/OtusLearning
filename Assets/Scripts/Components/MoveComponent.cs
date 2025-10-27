using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private float speed = 5.0f;
        private float _angelDegrees;
        private Vector2 _direct;

        public void SetDirectMove(Vector2 direct)
        {
            _direct = direct;
        }

        public void SetDirectLook(Vector2 direct)
        {
            var radian = Mathf.Atan2(direct.y, direct.x);
            _angelDegrees = Mathf.Rad2Deg * radian;
        }

        private void Update()
        {
            var nextPosition = rigidbody2D.position + _direct * speed;
            rigidbody2D.MovePosition(nextPosition);
            transform.rotation = Quaternion.Euler(0, 0, _angelDegrees);
        }
    }
}