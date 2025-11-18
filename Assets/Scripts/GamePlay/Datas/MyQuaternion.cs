using System;
using UnityEngine;

namespace GamePlay
{
    [Serializable]
    public struct MyQuaternion
    {
        public float x, y, z, w;

        public Quaternion GetQuaternion()
        {
            return new Quaternion(x, y, z, w);
        }

        public void Set(Quaternion quaternion)
        {
            x = quaternion.x;
            y = quaternion.y;
            z = quaternion.z;
            w = quaternion.w;
        }
    }
}