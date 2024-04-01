using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public class Inventory
{
    public readonly List<Item> ItemList;
    public int InventoryCapacity { get; private set; }
    public Action OnItemChange { get; set; }
    private const string KEY = "Inventory.dat";

    public Inventory(int inventoryCapacity)
    {
        new EquipItemsInventory();
        InventoryCapacity = inventoryCapacity;
        ItemList = new List<Item>();
        Delay();
    }

    private async void Delay()
    {
        await Task.Delay(100);
        Data.Instance.OnDataSave += SaveData;
        Data.Instance.DataSystem.Load<Dictionary<int, string>>(KEY, data =>
        {
            foreach (var dataItem in data)
            {
                ItemList.Add(new Item((ItemType)dataItem.Key, Convert.ToInt32(dataItem.Value)));
            }
            Debug.Log("Load [Inventory.dat]");
        });
        OnItemChange?.Invoke();
    }

    public bool TryGetAmmo()
    {
        int itemIndex = -1;
        if (ItemList.Count < 1)
            return false;

        for (int i = 0; i < ItemList.Count; i++)
            if (ItemList[i].CurrentItemType == ItemType.Ammo)
            {
                ItemList[i].SubtractQuantity(1);
                OnItemChange?.Invoke();
                itemIndex = i;
            }

        if (itemIndex == -1)
            return false;

        if (ItemList[itemIndex].Quantity <= 0)
        {
            ItemList.RemoveAt(itemIndex);
            OnItemChange?.Invoke();
        }

        return true;
    }

    public bool AddItem(Item newItem)
    {
        if (newItem.IsStackable())
            foreach (Item inventoryItem in ItemList)
                if (newItem.CurrentItemType == inventoryItem.CurrentItemType && inventoryItem.Quantity < inventoryItem.MaxCapacity() && newItem.Quantity > 0)
                {
                    int quantityToChange = 0;
                    while ((inventoryItem.Quantity + quantityToChange) < inventoryItem.MaxCapacity() && quantityToChange < newItem.Quantity)
                    {
                        quantityToChange++;
                    }

                    inventoryItem.AddQuantity(quantityToChange);
                    newItem.SubtractQuantity(quantityToChange);

                    OnItemChange?.Invoke();
                    if (newItem.Quantity <= 0) continue;
                }

        if (ItemList.Count < InventoryCapacity && newItem.Quantity > 0)
        {
            ItemList.Add(newItem);
            OnItemChange?.Invoke();
            return true;
        }
        else if (newItem.Quantity <= 0)
        {
            Debug.Log("Item quantity is zero. Destroy prefab");
            return true;
        }
        else
        {
            Debug.Log("Inventory is full");
            return false;
        }
    }

    public void RemoveItem(int index)
    => ItemList.RemoveAt(index);

    private void SaveData()
    {
        Dictionary<int, string> dictionaryInventoryData = new();
        foreach (var item in ItemList)
        {
            dictionaryInventoryData.TryAdd((int)item.CurrentItemType, item.Quantity.ToString());
        }

        Debug.Log("Save [inventory]");
        Data.Instance.DataSystem.Save(KEY, dictionaryInventoryData);
    }
}