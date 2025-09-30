using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "BulletConfig",
        menuName = "Bullets/New BulletConfig"
    )]
    public sealed class BulletConfig : ScriptableObject
    {
        public PhysicsLayer physicsLayer;
        public Color color;
        public int damage;
        public float speed;
        public bool isPlayer;
    }
}