using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField]
        private new Rigidbody2D rigidbody2D;

        [SerializeField]
        private float speed = 5.0f;

        private Vector2 _direct;

        public void SetDirectMove(Vector2 direct)
        {
            _direct = direct;
        }

        public void SetDirectLook(Vector2 direct)
        {
            var radian = Mathf.Atan2(direct.y, direct.x);
            var angelDegrees = Mathf.Rad2Deg * radian;
            
            transform.rotation = Quaternion.Euler(0,0,angelDegrees);
        }

        private void Update()
        {
            var nextPosition = this.rigidbody2D.position + _direct * this.speed;
            rigidbody2D.MovePosition(nextPosition);
        }
    }
}