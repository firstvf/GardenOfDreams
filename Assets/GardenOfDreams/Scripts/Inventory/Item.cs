using System;
using UnityEngine;

[Serializable]
public class Item
{
    [SerializeField] private ItemType _currentItemType;
    [SerializeField] private int _quantity = 1;

    public ItemType CurrentItemType => _currentItemType;
    public int Quantity => _quantity;

    public Item(ItemType type, int amount)
    {
        _currentItemType = type;
        _quantity = amount;
    }

    public void AddQuantity(int count)
    => _quantity += count;

    public void SubtractQuantity(int count)
    => _quantity -= count;

    public int MaxCapacity()
    {
        switch (_currentItemType)
        {
            case ItemType.Ammo:
                return 120;
            default:
                return 1;
        }
    }

    public bool IsStackable()
    {
        switch (_currentItemType)
        {
            case ItemType.Ammo:
                return true;
            default:
                return false;
        }
    }
}

public enum ItemType
{
    Ammo,
    Ak74,
    Makarov,
    BanditPants,
    Cloak,
    Elbow,
    Wrist,
    MillitaryHelmet,
    Bag
}