using Extensions;
using Inventory;
using TriInspector;
using UnityEngine;
using Zenject;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private ItemVisual _prefab;
    [SerializeField] private GameObject _itemsContainer;
    [Inject] private DiContainer _diContainer;
    [SerializeField]private Player _player;

    public void SetPlayer(Player player)
    {
        _player = player;
        RebuildInventory();
    }

    [Button]
    private void RebuildInventory()
    {
        _itemsContainer.transform.DestroyAll();
        for (int i = 0; i < _player.inventory.items.Count; i++)
        {
            var obj = _diContainer.InstantiatePrefab(_prefab, _itemsContainer.transform);
            obj.GetComponent<ItemVisual>().SetItem(_player.inventory.items[i]);
        }
    }
}
