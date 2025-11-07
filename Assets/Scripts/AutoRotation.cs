using UnityEngine;

public class AutoRotation : MonoBehaviour
{
    [SerializeField] private GameObject _rotationGameObject;
    [SerializeField] private float _rotationSpeed = 360;
    
    void Update()
    {
        _rotationGameObject.transform.Rotate(Vector3.forward, Time.deltaTime * _rotationSpeed);
    }
}
