using Installers;
using Timer;
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
        
        EventTimerInstaller.Install(_sceneContext.Container);
        ItemGeneratorInstaller.Install(_sceneContext.Container);
        PlayerInstaller.Install(_sceneContext.Container);
    }
}