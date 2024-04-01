using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] ItemPrefab[] _items;
    public static Drop Instance { get; private set; }

    private void Awake()
    => Init();

    public ItemPrefab DropItem()
    => _items[Random.Range(0, _items.Length)];

    private void Init()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(gameObject);
    }
}