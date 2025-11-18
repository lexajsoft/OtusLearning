using System;
using UnityEngine;

namespace GamePlay
{
    [Serializable]
    public struct MyVector3
    {
        public float x, y, z;

        public MyVector3(float x,float y,float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        
        public Vector3 GetVector3()
        {
            return new Vector3(x, y, z);
        }

        public void Set(Vector3 vector3)
        {
            x = vector3.x;
            y = vector3.y;
            z = vector3.z;
        }
    }
}