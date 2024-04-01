using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private GameObject _objectToRotate;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    => _rigidbody2D = GetComponent<Rigidbody2D>();

    private void Update()
    {
        if (Player.Instance.AttackSystem.IsAlive && Player.Instance.IsJoystickSet)
            Move();
    }

    private void Move()
    {
        var horizontalInput = Player.Instance.Joystick.Horizontal;
        var verticalInput = Player.Instance.Joystick.Vertical;

        if (horizontalInput != 0 || verticalInput != 0)
        {
            var movePosition = new Vector3(horizontalInput, verticalInput).normalized * _movementSpeed;
            _rigidbody2D.MovePosition(transform.position + (movePosition * Time.fixedDeltaTime));

            Rotate(horizontalInput);
        }
    }

    private void Rotate(float horizontalInput)
    {
        if (horizontalInput > 0 && _objectToRotate.transform.rotation.y != 0)
            _objectToRotate.transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (horizontalInput < 0 && _objectToRotate.transform.rotation.y != 1 && _objectToRotate.transform.rotation.y != -1)
            _objectToRotate.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}