using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    [SerializeField] private EquipItemsInventory_UI _equipItemsUI;
    [SerializeField] private Transform _inventorySlotsContainer;
    private List<ItemSlotContainer> _inventorySlots;
    private Inventory _inventory;
    private bool _isInitReady;

    private void Awake()
    {
        _inventorySlots = new List<ItemSlotContainer>();
        _inventorySlots.AddRange(GetComponentsInChildren<ItemSlotContainer>());
    }

    private void OnEnable()
    {
        if (_isInitReady)
        {
            RefreshInventory();
            _inventory.OnItemChange += RefreshInventory;
        }
    }

    private void Start()
    => InitInventoryUI();

    public void RemoveItemFromSlot(int index)
    {
        _inventory.RemoveItem(index);
        RefreshInventory();
    }

    private void InitInventoryUI()
    {
        if (_inventory == null)
        {
            _inventory = Player.Instance.Inventory;
            _inventory.OnItemChange += RefreshInventory;
            for (int i = 0; i <= _inventory.InventoryCapacity - _inventorySlots.Count; i++)
            {
                _inventorySlots.Add(Instantiate(_inventorySlots[0], _inventorySlotsContainer));
                i = 0;
            }

            for (int i = 0; i < _inventorySlots.Count; i++)
                _inventorySlots[i].gameObject.SetActive(true);

            _isInitReady = true;
            RefreshInventory();
            _inventorySlotsContainer.gameObject.SetActive(false);
        }
    }

    private void RefreshInventory()
    {

        foreach (var slot in _inventorySlots)
            slot.RefreshItemSlot();

        for (int i = 0; i < _inventory.ItemList.Count; i++)
        {
            if (_equipItemsUI.IsEmptyEquipmentSlot(_inventory.ItemList[i]))
            {
                _inventory.RemoveItem(i);
                break;
            }
        }

        foreach (var item in _inventory.ItemList)
        {
            for (int i = 0; i < _inventorySlots.Count; i++)
                if (_inventorySlots[i].IsSlotEmpty)
                {

                    _inventorySlots[i].SetItemInSlot(i,
                        ItemSpriteInfo.Instance.GetItemSprite(item.CurrentItemType), item.Quantity);
                    break;
                }
        }
    }

    private void OnDisable()
    => _inventory.OnItemChange -= RefreshInventory;
}