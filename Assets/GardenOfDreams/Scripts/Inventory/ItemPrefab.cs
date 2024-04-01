using UnityEngine;

public class ItemPrefab : MonoBehaviour
{
    [SerializeField] private Item _item;

    public Item GetItem => _item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if (player.TryPickupItem(_item))
            {
                Destroy(gameObject);
                Debug.Log("Pickup item: " + _item.CurrentItemType);
            }
        }
    }
}