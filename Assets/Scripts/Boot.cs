using System;
using Configs;
using Installers;
using UnityEngine;
using Zenject;

[DefaultExecutionOrder(-1000)]
public class Boot : MonoBehaviour
{
     [SerializeField] private SceneContext _sceneContext;

    private void Awake()
    {
        var container = _sceneContext.Container;
        _sceneContext.Run();

        ItemGeneratorInstaller.Install(_sceneContext.Container);
        PlayerInstaller.Install(_sceneContext.Container);
        
    }
}