using UnityEngine;

public class PlayerEquipSlot : MonoBehaviour
{
    private Transform _container;
    private ItemPrefab _item;
    private bool _isSlotEmpty = true;

    private void Awake()
    => _container = GetComponent<Transform>();

    public bool TryEquipItem(ItemPrefab item)
    {
        if (!_isSlotEmpty)
            return false;

        _isSlotEmpty = false;
        _item = Instantiate(item, _container);
        Destroy(_item.gameObject.GetComponent<Rigidbody2D>());
        Destroy(_item.gameObject.GetComponent<Collider2D>());

        if (GetWeapon() != null)
            Player.Instance.AttackSystem.SetWeapon(GetWeapon());

        _item.transform.localPosition = new Vector2(0, 0);

        return true;
    }

    public Weapon GetWeapon()
    {
        switch (_item.GetItem.CurrentItemType)
        {
            case ItemType.Ak74:
                return _item.GetComponent<Weapon>();
            case ItemType.Makarov:
                return _item.GetComponent<Weapon>();
            default:
                return null;
        }
    }

    public void RemoveItem(Item item)
    {
        ///
    }
}