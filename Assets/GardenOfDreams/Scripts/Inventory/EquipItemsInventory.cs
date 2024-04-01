using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EquipItemsInventory
{
    public static EquipItemsInventory Instance { get; set; }
    public readonly List<Item> EquipItemsList;
    public Action<Item> OnEquipItemHandler { get; set; }
    public Action<Item> OnRemoveItemHandler { get; set; }
    public Action OnLoadInventory { get; set; }

    private const string KEY = "EquipItems.dat";

    public EquipItemsInventory()
    {
        Instance = this;
        EquipItemsList = new List<Item>();
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
                EquipItemsList.Add(new Item((ItemType)dataItem.Key, Convert.ToInt32(dataItem.Value)));
            }
            Debug.Log("Load [EquipItems.dat]");
        });
        OnLoadInventory?.Invoke();
    }

    public void AddEquipItem(Item item)
    {
        EquipItemsList.Add(item);
        OnEquipItemHandler?.Invoke(item);
    }

    public void RemoveEquipItem(Item item)
    {
        EquipItemsList.Remove(item);
        OnRemoveItemHandler?.Invoke(item);
    }

    private void SaveData()
    {
        Dictionary<int, string> dictionaryInventoryData = new();
        foreach (var item in EquipItemsList)
            dictionaryInventoryData.TryAdd((int)item.CurrentItemType, item.Quantity.ToString());

        Debug.Log("Save equip inventory");
        Data.Instance.DataSystem.Save(KEY, dictionaryInventoryData);
    }
}