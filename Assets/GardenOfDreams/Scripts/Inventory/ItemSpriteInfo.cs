using UnityEngine;

public class ItemSpriteInfo : MonoBehaviour
{
    public static ItemSpriteInfo Instance { get; private set; }

    [SerializeField] private Sprite _spriteAmmo;
    [SerializeField] private Sprite _spriteAk74;
    [SerializeField] private Sprite _spriteMakarov;
    [SerializeField] private Sprite _spriteBanditPants;
    [SerializeField] private Sprite _spriteCloak;
    [SerializeField] private Sprite _spriteElbow;
    [SerializeField] private Sprite _spriteWrist;
    [SerializeField] private Sprite _spriteMillitaryHelmet;
    [SerializeField] private Sprite _spriteBag;

    private void Awake()
    => Init();

    public Sprite GetItemSprite(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Ammo:
                return _spriteAmmo;
            case ItemType.Ak74:
                return _spriteAk74;
            case ItemType.Makarov:
                return _spriteMakarov;
            case ItemType.BanditPants:
                return _spriteBanditPants;
            case ItemType.Cloak:
                return _spriteCloak;
            case ItemType.Elbow:
                return _spriteElbow;
            case ItemType.Wrist:
                return _spriteWrist;
            case ItemType.MillitaryHelmet:
                return _spriteMillitaryHelmet;
            case ItemType.Bag:
                return _spriteBag;
            default:
                return null;
        }
    }

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