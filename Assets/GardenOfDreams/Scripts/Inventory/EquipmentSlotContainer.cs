using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotContainer : MonoBehaviour
{
    [SerializeField] private Image _imageContainer;
    private bool _isSlotEmpty = true;
    private Item _currentItem;

    public bool TrySetItemInSlot(Item item)
    {
        if (!_isSlotEmpty)
            return false;

        _isSlotEmpty = false;
        EquipItemsInventory.Instance.AddEquipItem(item);
        _imageContainer.sprite = ItemSpriteInfo.Instance.GetItemSprite(item.CurrentItemType);
        _imageContainer.enabled = true;
        return true;

    }

    public void RemoveItemFromSlot()
    {
        _isSlotEmpty = true;
        _imageContainer.enabled = false;
        EquipItemsInventory.Instance.RemoveEquipItem(_currentItem);
        _currentItem = null;
    }
}