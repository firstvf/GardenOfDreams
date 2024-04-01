using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotContainer : MonoBehaviour
{
    [SerializeField] private GameObject _deleteButton;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Inventory_UI _inventoryUI;
    public Image SlotContainer { get; private set; }
    public bool IsSlotEmpty { get; private set; }

    private int _itemIndex = -1;

    private void Awake()
    {
        SlotContainer = GetComponent<Image>();
        IsSlotEmpty = true;
        _text.enabled = false;
        SlotContainer.enabled = false;
    }

    public void SetItemInSlot(int itemIndex, Sprite itemSprite, int amount)
    {
        _itemIndex = itemIndex;
        SlotContainer.overrideSprite = itemSprite;
        IsSlotEmpty = false;
        SlotContainer.enabled = true;
        if (amount > 1)
            _text.text = amount.ToString();
        _text.enabled = true;
    }

    public void RefreshItemSlot()
    {
        _text.text = "";
        _itemIndex = -1;
        IsSlotEmpty = true;
        SlotContainer.enabled = false;
        _text.enabled = false;
        _deleteButton.SetActive(false);
    }

    public void RemoveItemFromSlot()
    {
        if (_itemIndex != -1)
            _inventoryUI.RemoveItemFromSlot(_itemIndex);
    }

    public void DeleteButton()
    {
        if (_itemIndex != -1)
        {
            _deleteButton.SetActive(true);
            CloseDeleteButtonTimer();
        }
    }

    private async void CloseDeleteButtonTimer()
    {
        await Task.Delay(2 * 1000);
        _deleteButton.SetActive(false);
    }
}