using System.Threading.Tasks;
using UnityEngine;

public class EquipItemsInventory_UI : MonoBehaviour
{
    [SerializeField] private EquipmentSlotContainer _heatContainer;
    [SerializeField] private EquipmentSlotContainer _bodyContainer;
    [SerializeField] private EquipmentSlotContainer[] _armContainer;
    [SerializeField] private EquipmentSlotContainer[] _wristContainer;
    [SerializeField] private EquipmentSlotContainer[] _legContainer;
    [SerializeField] private EquipmentSlotContainer _backpackContainer;
    [SerializeField] private EquipmentSlotContainer _weaponContainer;

    private void Awake()
    => EquipItemsInventory.Instance.OnLoadInventory += LoadInvnetory;

    private void LoadInvnetory()
    {
        for (int i = 0; i < EquipItemsInventory.Instance.EquipItemsList.Count; i++)
            IsEmptyEquipmentSlot(EquipItemsInventory.Instance.EquipItemsList[i]);
    }

    public bool IsEmptyEquipmentSlot(Item item)
    {
        switch (item.CurrentItemType)
        {
            case ItemType.Ak74:
                return _weaponContainer.TrySetItemInSlot(item);
            case ItemType.Makarov:
                return _weaponContainer.TrySetItemInSlot(item);
            case ItemType.BanditPants:
                for (int i = 0; i < _legContainer.Length; i++)
                {
                    if (_legContainer[i].TrySetItemInSlot(item))
                        return true;
                }
                return false;
            case ItemType.Cloak:
                return _bodyContainer.TrySetItemInSlot(item);
            case ItemType.Elbow:
                for (int i = 0; i < _armContainer.Length; i++)
                {
                    if (_armContainer[i].TrySetItemInSlot(item))
                        return true;
                }
                return false;
            case ItemType.Wrist:
                for (int i = 0; i < _wristContainer.Length; i++)
                {
                    if (_wristContainer[i].TrySetItemInSlot(item))
                        return true;
                }
                return false;
            case ItemType.MillitaryHelmet:
                return _heatContainer.TrySetItemInSlot(item);
            case ItemType.Bag:
                return _backpackContainer.TrySetItemInSlot(item);
            default:
                return false;
        }
    }
}