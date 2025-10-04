using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using Inventory;
using Inventory.Components;
using TriInspector;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PlayerInventory : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;

    [SerializeField] private ItemVisual _prefab;
    [SerializeField] private GameObject _itemsContainer;

    private Player _player;

    // инвентарь игрока
    public Inventory.Inventory Inventory => _player != null? _player.inventory : null;

    // Созданные предметы
    private Dictionary<Item, ItemVisual> _createdItemVisuals;

    // Слоты текущие у игрока
    private Dictionary<EquipSlot, ItemSlot> _equippedItemSlots = new();

    // указывается слоты на форме 
    [SerializeField] private List<ItemSlot> _prepareItemSlots;

    public Player Player => _player;

    public UnityAction<ItemVisual> OnItemVisualClicked;
    public UnityAction<ItemVisual> OnItemSlotVisualClicked;
    public UnityAction<Player> OnPlayerChanged;

    public void SetPlayer(Player player)
    {
        PrepareItemSlotVisuals(player);

        _createdItemVisuals = new Dictionary<Item, ItemVisual>();
        DestroyItemVisuals();
        
        DeActivateListener();
        _player = player;
        ActivateListener();
        RebuildInventory();
        
        OnPlayerChanged?.Invoke(_player);
    }

    private void PrepareItemSlotVisuals(Player player)
    {
        foreach (var itemSlot in _prepareItemSlots) itemSlot.gameObject.SetActive(false);

        // указываются какие слоты потребуется использовать в зависимости от инвентаря игрока
        _equippedItemSlots = new Dictionary<EquipSlot, ItemSlot>();
        foreach (var equip in player.inventory.equipped)
        {
            var list = _prepareItemSlots.Where(item => item.EquipSlot == equip.Key).ToList();
            if (list.Count > 0)
            {
                _equippedItemSlots[equip.Key] = list[0];
                list[0].gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError($"Не удалось найти в списке нужный слот для типа [{equip.Key}]");
            }
        }
    }

    private void Start()
    {
        for (var i = 0; i < _prepareItemSlots.Count; i++)
        {
            _prepareItemSlots[i].OnClicked += OnItemSlotVisualClicked;
        }
    }

    private void OnEnable()
    {
        if (_player == null)
            return;
    }

    private void OnDisable()
    {
        DeActivateListener();
    }

    private void ActivateListener()
    {
        if(Inventory == null)
            return;
        
        Inventory.OnItemAdded += OnItemAdded;
        Inventory.OnItemRemoved += OnItemRemoved;
        Inventory.OnEquippedItem += OnEquippedItem;
        Inventory.OnUnEquippedItem += OnUnEquippedItem;
        Inventory.OnItemUpdated += OnItemUpdated;
    }

    private void OnItemUpdated(Item obj)
    {
        _createdItemVisuals[obj].Refresh();
    }

    private void OnUnEquippedItem(EquipSlot slot)
    {
        if (_equippedItemSlots.TryGetValue(slot, out var itemSlot)) itemSlot.SetItem(null);
    }

    private void OnEquippedItem(EquipSlot slot, Item item)
    {
        if (_equippedItemSlots.TryGetValue(slot, out var itemSlot)) itemSlot.SetItem(item);
    }

    private void DeActivateListener()
    {
        if(Inventory == null)
            return;
        
        Inventory.OnItemAdded -= OnItemAdded;
        Inventory.OnItemRemoved -= OnItemRemoved;
        Inventory.OnEquippedItem -= OnEquippedItem;
        Inventory.OnUnEquippedItem -= OnUnEquippedItem;
        Inventory.OnItemUpdated -= OnItemUpdated;
    }

    private void OnItemRemoved(Item item)
    {
        if (_createdItemVisuals.ContainsKey(item))
        {
            if (_createdItemVisuals[item] != null) Destroy(_createdItemVisuals[item].gameObject);

            _createdItemVisuals.Remove(item);
        }
    }

    private void OnItemAdded(Item item)
    {
        CreateItemVisual(item);
    }


    [Button]
    private void RebuildInventory()
    {
        DestroyItemVisuals();
        for (var i = 0; i < _player.inventory.items.Count; i++)
        {
            CreateItemVisual(_player.inventory.items[i]);
        }

        // очистка одетых слотов
        foreach (var itemSlot in _equippedItemSlots)
        {
            itemSlot.Value.SetItem(null);
        }

        // накидывание на слоты одетые шмотки если таковы имеются
        foreach (var equip in _player.inventory.equipped)
        {
            _equippedItemSlots[equip.Key].SetItem(equip.Value);
        }
    }


    private void CreateItemVisual(Item item)
    {
        var obj = _diContainer.InstantiatePrefab(_prefab, _itemsContainer.transform);
        var itemVisual = obj.GetComponent<ItemVisual>();
        itemVisual.SetItem(item);
        itemVisual.OnClicked += OnItemClicked;
        _createdItemVisuals[item] = itemVisual;
    }

    private void DestroyItemVisuals()
    {
        _itemsContainer.transform.DestroyAll();
    }

    private void OnItemClicked(ItemVisual itemVisual)
    {
        OnItemVisualClicked?.Invoke(itemVisual);
    }
}