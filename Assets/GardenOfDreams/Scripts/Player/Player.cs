using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerAttackSystem))]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public FixedJoystick Joystick { get; private set; }
    public PlayerAttackSystem AttackSystem { get; private set; }
    public bool IsJoystickSet { get; private set; }
    public Inventory Inventory { get; private set; }

    
    private void Awake()
    {
        Init();
        AttackSystem = GetComponent<PlayerAttackSystem>();
        Inventory = new Inventory(16);
    }

    public void SetJoystick(FixedJoystick joystick)
    {
        Joystick = joystick;
        IsJoystickSet = true;
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

    public bool TryPickupItem(Item item)
    => Inventory.AddItem(item);
}