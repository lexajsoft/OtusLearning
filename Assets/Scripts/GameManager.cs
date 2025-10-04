using System.Collections;
using Inventory;
using TriInspector;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private StatsVisual _statsVisual;
    
    private Player _player;

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
         _player = _diContainer.Resolve<Player>();
         RebuildRandomPlayerInventory();
    }

    [Button]
    public void RebuildRandomPlayerInventory()
    {
        _player.Init();
        _inventory.SetPlayer(_player);
        _statsVisual.SetPlayer(_player);
    }
    
    [Button]
    public void AddRandomItemToInventory()
    {
        _player.AddTestArmor();
    }
    
    [Button]
    public void AddRandomItemEtc()
    {
        _player.AddEtc();
    }
    
    [Button]
    public void AddRandomItemToTalisman()
    {
        _player.AddTalisman();
    }
}
