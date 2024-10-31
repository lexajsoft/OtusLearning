using UnityEngine;

namespace PresentationModel.Scripts
{
    public class AutoRotation : MonoBehaviour
    {
        public float _speed = 1;
        // Update is called once per frame
        void Update()
        {
            var rotation = transform.rotation;
            rotation = Quaternion.Euler(rotation.eulerAngles.x,rotation.eulerAngles.y,rotation.eulerAngles.z + _speed*Time.deltaTime);
            transform.rotation = rotation;
        }
    }
}
