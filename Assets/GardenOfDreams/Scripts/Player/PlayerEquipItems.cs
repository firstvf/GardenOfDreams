using UnityEngine;

public class PlayerEquipItems : MonoBehaviour
{
    [SerializeField] private ItemPrefab[] _items;

    [SerializeField] private PlayerEquipSlot _gunSlot;
    [SerializeField] private PlayerEquipSlot _pistolSlot;
    [SerializeField] private PlayerEquipSlot _heatSlot;
    [SerializeField] private PlayerEquipSlot[] _armSlot;
    [SerializeField] private PlayerEquipSlot[] _wristSlot;
    [SerializeField] private PlayerEquipSlot[] _legSlot;
    [SerializeField] private PlayerEquipSlot _backpackSlot;
    [SerializeField] private PlayerEquipSlot _bodySlot;

    private void Start()
    => EquipItemsInventory.Instance.OnEquipItemHandler += EquipItem;

    public void EquipItem(Item item)
    {
        switch (item.CurrentItemType)
        {
            case ItemType.Ak74:
                foreach (var itemPrefab in _items)
                    if (itemPrefab.GetItem.CurrentItemType == item.CurrentItemType)
                        _gunSlot.TryEquipItem(itemPrefab);
                break;
            case ItemType.Makarov:
                foreach (var itemPrefab in _items)
                    if (itemPrefab.GetItem.CurrentItemType == item.CurrentItemType)                    
                        _pistolSlot.TryEquipItem(itemPrefab);                    
                break;
            case ItemType.BanditPants:
                foreach (var itemPrefab in _items)
                    for (int i = 0; i < _legSlot.Length; i++)
                        if (itemPrefab.GetItem.CurrentItemType == item.CurrentItemType)
                        {
                            if (_legSlot[i].TryEquipItem(itemPrefab))
                                return;
                        }
                break;
            case ItemType.Cloak:
                foreach (var itemPrefab in _items)
                    if (itemPrefab.GetItem.CurrentItemType == item.CurrentItemType)
                        _bodySlot.TryEquipItem(itemPrefab);
                break;
            case ItemType.Elbow:
                foreach (var itemPrefab in _items)
                    for (int i = 0; i < _armSlot.Length; i++)
                        if (itemPrefab.GetItem.CurrentItemType == item.CurrentItemType)
                        {
                            if (_armSlot[i].TryEquipItem(itemPrefab))
                                return;
                        }
                break;
            case ItemType.Wrist:
                foreach (var itemPrefab in _items)
                    for (int i = 0; i < _wristSlot.Length; i++)
                        if (itemPrefab.GetItem.CurrentItemType == item.CurrentItemType)
                        {
                            if (_wristSlot[i].TryEquipItem(itemPrefab))
                                return;
                        }
                break;
            case ItemType.MillitaryHelmet:
                foreach (var itemPrefab in _items)
                    if (itemPrefab.GetItem.CurrentItemType == item.CurrentItemType)
                        _heatSlot.TryEquipItem(itemPrefab);
                break;
            case ItemType.Bag:
                foreach (var itemPrefab in _items)
                    if (itemPrefab.GetItem.CurrentItemType == item.CurrentItemType)
                        _backpackSlot.TryEquipItem(itemPrefab);
                break;
            default:
                break;
        }
    }
}