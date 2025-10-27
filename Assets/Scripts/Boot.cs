using System;
using ShootEmUp;
using UnityEngine;
using Zenject;

[DefaultExecutionOrder(-1000)]
public class Boot : MonoBehaviour
{
    [SerializeField] private SceneContext _sceneContext;
    private void Awake()
    {
        _sceneContext.Run();
        InputManagerInstaller2.Install(_sceneContext.Container);
    }
}