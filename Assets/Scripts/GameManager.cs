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

    public void Start()
    {
         _player = _diContainer.Resolve<Player>();
         RebuildRandomPlayerInventory();
    }

    private void RebuildRandomPlayerInventory()
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
    
    [Button]
    public void AddHealBottle()
    {
        _player.AddBottleHealth();
    }  
    
    [Button]
    public void AddManaBottle()
    {
        _player.AddBottleMana();
    }  
    
    [Button]
    public void AddComplexBottle()
    {
        _player.AddBottleHealthAndMana();
    }  
    
    [Button]
    public void AddRepairKit()
    {
        _player.AddRepairKit();
    }

    [Button]
    public void Damage(int value)
    {
        _player.Damage(value);
    }

    [Button]
    public void WasteMana(int value)
    {
        _player.WastMana(value);
    }

}
