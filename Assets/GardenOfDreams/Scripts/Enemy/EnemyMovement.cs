using UnityEngine;

[RequireComponent(typeof(Enemy), typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private GameObject _objectToRotate;

    private Enemy _enemy;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    => MoveToTarget();

    private void MoveToTarget()
    {
        if (_enemy.IsTargetSet && _enemy.State.IsAbleToMove() && Vector2.Distance(transform.position, _enemy.Target.position) >= _enemy.AttackSystem.AttackRange)
        {
            Vector2 movePosition = Vector2.MoveTowards(transform.position, _enemy.Target.transform.position, _movementSpeed * Time.fixedDeltaTime);
            _rigidbody2D.MovePosition(movePosition);

            Rotate();
        }
    }

    private void Rotate()
    {
        Vector2 direction = (_enemy.Target.transform.position - transform.position).normalized;

        if (direction.x > 0 && _objectToRotate.transform.rotation.y != 0)
            _objectToRotate.transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (direction.x < 0 && _objectToRotate.transform.rotation.y != 1 && _objectToRotate.transform.rotation.y != -1)
            _objectToRotate.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}