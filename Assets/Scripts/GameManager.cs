using System.Collections;
using TriInspector;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;
    [SerializeField] private PlayerInventory _inventory;
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
        _player.CreateRandomInventory();
        _inventory.SetPlayer(_player);
    }
}
