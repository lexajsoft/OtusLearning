using System;
using System.Net;
using Extensions;
using Inventory;
using Inventory.Components;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ItemMenuHandler : MonoBehaviour
{
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private GameObject _blockArea;
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _menuContainer;
    
    [SerializeField] private GameObject _separatorPrefab;
    [SerializeField] private GameObject _invisibleSeparatorPrefab;
    
    [SerializeField] private MenuButton _menuButtonPrefab;
    [SerializeField] private MenuText _titleMenuTextPrefab;
    [SerializeField] private MenuText _statsTitleMenuTextPrefab;
    [SerializeField] private MenuText _effectDescriptionMenuTextPrefab;
    [SerializeField] private MenuDoubleText _statMenuTextPrefab;
    [SerializeField] private Button _closeButton;
    
    private bool _isOpened = false;
    private ItemVisual _itemVisual;

    private void OnEnable()
    {
        _playerInventory.OnItemVisualClicked += OnItemVisualClicked;
        _playerInventory.OnItemSlotVisualClicked += OnItemSlotVisualClicked;
        _closeButton.onClick.AddListener(HideMenu);
        _blockArea.GetComponent<Button>().onClick.AddListener(HideMenu);
    }
    private void OnDisable()
    {
        _playerInventory.OnItemVisualClicked -= OnItemVisualClicked;
        _playerInventory.OnItemSlotVisualClicked -= OnItemSlotVisualClicked;
        _closeButton.onClick.RemoveListener(HideMenu);
        _blockArea.GetComponent<Button>().onClick.RemoveListener(HideMenu);
    }
    private void OnItemVisualClicked(ItemVisual itemVisual)
    {
        if (_isOpened)
        {
            HideMenu();
        }
        else
        {
            _itemVisual = itemVisual;
            ShowMenu();
        }
    }
    
    private void OnItemSlotVisualClicked(ItemVisual itemVisual)
    {
        if (_isOpened)
        {
            HideMenu();
        }
        else
        {
            _itemVisual = itemVisual;
            ShowMenuUnEquip();
        }
    }

    private void ShowMenuUnEquip()
    {
        if(_itemVisual.Item == null)
            return;
        
        // Активация блок зоны
        _blockArea.SetActive(true);
        
        _menu.SetActive(true);
        _menuContainer.transform.DestroyAll();

        // перемещение к карточке
        _menu.transform.position = _itemVisual.transform.position;
        
        CreateText(_itemVisual.Item.name, _titleMenuTextPrefab);
        CreateSeparator(_separatorPrefab);
        if(CreateStatsBlock())
            CreateSeparator(_separatorPrefab);
        CreateEquipAndUnEquipMenuButton();
        //CreateSeparator(_separatorPrefab);
        //CreateButton("Закрыть", HideMenu);
    }

    private void HideMenu()
    {
        _isOpened = false;
        _blockArea.SetActive(false);
        _menu.SetActive(false);
    }

    private void ShowMenu()
    {
        if(_itemVisual.Item == null)
            return;
        
        // Активация блок зоны
        _blockArea.SetActive(true);
        
        _menu.SetActive(true);
        _menuContainer.transform.DestroyAll();

        // перемещение к карточке
        _menu.transform.position = _itemVisual.transform.position;
        
        CreateText(_itemVisual.Item.name, _titleMenuTextPrefab);
        CreateSeparator(_separatorPrefab);
        if(CreateStatsBlock())
            CreateSeparator(_separatorPrefab);
        if(CreateEquipAndUnEquipMenuButton())
            CreateSeparator(_separatorPrefab);
        if (CreateUsableMenuButton())
        {
            CreateSeparator(_invisibleSeparatorPrefab); 
            if (_itemVisual.Item.IsCanUseItem())
            {
                CreateButton("Использовать", ()=>
                {
                    UseItem();
                    HideMenu();
                });
            }
            else
            {
                CreateButton("Использовать", null, false);
            }
            CreateSeparator(_separatorPrefab);
        }

        CreateButton("Удалить", ()=>
        {
            DeleteItem();
            HideMenu();
        });
        CreateSeparator(_invisibleSeparatorPrefab);
        
        if(IsCanDamageItem())
        {
            CreateButton("Поломать", () =>
            {
                DamageItem();
                HideMenu();
            });
        }
        //CreateSeparator(_invisibleSeparatorPrefab);
        //CreateButton("Закрыть", HideMenu);
    }

    private void DamageItem()
    {
        if (IsCanDamageItem())
        {
            _itemVisual.Item.GetComponent<DurabilityComponent>().ReduceDurability(Random.Range(1,20));
        }
    }
    
    private bool IsCanDamageItem()
    {
        if (_itemVisual.Item.HasComponent<DurabilityComponent>() && _itemVisual.Item.HasComponent<EquipableComponent>())
        {
            return true;
        }

        return false;
    }

    private void UseItem()
    {
        _itemVisual.Item.UseItem();
    }

    private bool CreateUsableMenuButton()
    {
        if (_itemVisual.Item.HasComponent<UsableComponent>())
        {
            var usebleComponent = _itemVisual.Item.GetComponent<UsableComponent>();
            CreateText("Эффекты", _titleMenuTextPrefab);
            for (int i = 0; i < usebleComponent.Effects.Count; i++)
            {
                CreateTextUsable($"При использовании : {usebleComponent.Effects[i].GetDescription()}", _effectDescriptionMenuTextPrefab);
            }
            return true;
        }
        return false;
    }

    private void DeleteItem()
    {
        _playerInventory.Inventory.RemoveItem(_itemVisual.Item);
    }

    private bool CreateStatsBlock()
    {
        bool isAny = false;
        // Оружие
        if (_itemVisual.Item.HasComponent<WeaponComponent>())
        {
            var weaponComponent = _itemVisual.Item.GetComponent<WeaponComponent>();
                CreateText("Атака", _statsTitleMenuTextPrefab);
                string atkText = $"{weaponComponent.minMaxDamage.x} - {weaponComponent.minMaxDamage.y}";
                string cooldown = $"{(1f / weaponComponent.cooldown).ToString("F2")}";
                
                CreateDoubleText("Урон", atkText, _statMenuTextPrefab);
                CreateDoubleText("Скорость", cooldown, _statMenuTextPrefab);
                isAny = true;
        }
        
        // Статы
        if (_itemVisual.Item.HasComponent<StatsComponent>())
        {
            var statsComponent = _itemVisual.Item.GetComponent<StatsComponent>();
            if (statsComponent.properties.Count > 0)
            {
                CreateText("Характеристики", _statsTitleMenuTextPrefab);
                for (int i = 0; i < statsComponent.properties.Count; i++)
                {
                    CreateDoubleText(statsComponent.properties[i].stat.ToString(),statsComponent.properties[i].Value.ToString(), _statMenuTextPrefab);
                }
                isAny = true;
            }
        }
        return isAny ;
    }

    private bool CreateEquipAndUnEquipMenuButton()
    {
        if (_itemVisual.Item.HasComponent<EquipableComponent>())
        {
            var equipableComponent = _itemVisual.Item.GetComponent<EquipableComponent>();
            if (equipableComponent.IsEquiped)
            {
                CreateButton("Снять", () =>
                {
                    _playerInventory.Inventory.UnEquipItem(equipableComponent);
                    HideMenu();
                });
            }
            else
            {
                CreateButton("Надеть", () =>
                {
                    _playerInventory.Inventory.EquipItem(equipableComponent);
                    HideMenu();
                });
                
            }
            return true;
        }
        return false;
        
    }

    private void CreateButton(string text, Action clickMenu, bool isInteracted = true)
    {
        var menuButton = Instantiate(_menuButtonPrefab, _menuContainer.transform);
        menuButton.SetText(text);
        menuButton.OnClicked += clickMenu;
        menuButton.SetInteractable(isInteracted);
    }

    private void CreateText(string text, MenuText prefab)
    {
        var menuText = Instantiate(prefab, _menuContainer.transform);
        menuText.SetText(text);
    }
    
    private void CreateTextUsable(string text, MenuText prefab)
    {
        var menuText = Instantiate(prefab, _menuContainer.transform);
        menuText.SetText(text);
    }
    
    
    private void CreateDoubleText(string text1, string text2, MenuDoubleText prefab)
    {
        var menuText = Instantiate(prefab, _menuContainer.transform);
        menuText.SetText1(text1);
        menuText.SetText2(text2);
    }

    private void CreateSeparator(GameObject prefab)
    {
        Instantiate(prefab, _menuContainer.transform);
    }
}
