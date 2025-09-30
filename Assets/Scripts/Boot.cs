using System;
using UnityEngine;
using Zenject;

[DefaultExecutionOrder(-1000)]
public class Boot : MonoBehaviour
{
    [SerializeField] private SceneContext _sceneContext;
    private void Awake()
    {
        _sceneContext.Run();
    }
}