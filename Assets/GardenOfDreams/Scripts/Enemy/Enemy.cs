using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(EnemyAttackSystem), typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _findTargetDistance;
    [SerializeField] private float _timeToSearchTarget;

    public AttackSystem AttackSystem { get; private set; }
    public EnemyMovement Movement { get; private set; }
    public SimpleStateController State { get; private set; }
    public Transform Target { get; private set; }
    public bool IsTargetSet { get; private set; }

    private void Awake()
    {
        AttackSystem = GetComponent<AttackSystem>();
        Movement = GetComponent<EnemyMovement>();
        if (State == null)
            State = new SimpleStateController();
    }

    private void OnEnable()
    {
        CheckTargetDistance();
    }

    private void OnDrawGizmos()
    => Gizmos.DrawWireSphere(transform.position, _findTargetDistance);

    private async void CheckTargetDistance()
    {
        await Task.Delay(1000);

        while (AttackSystem.CurrentHealth > 0 && Player.Instance.AttackSystem.IsAlive)
        {
            await Task.Delay((int)(_timeToSearchTarget * 1000));
            if (!Application.isPlaying) return;

            if (Vector2.Distance(transform.position, Player.Instance.transform.position) <= _findTargetDistance
                && Player.Instance.AttackSystem.IsAlive)
            {
                Target = Player.Instance.transform;
                IsTargetSet = true;
            }
            else
            {
                if (IsTargetSet)
                {
                    IsTargetSet = false;
                    Target = null;
                }
            }
        }

        IsTargetSet = false;
        Target = null;
    }

    private void OnDisable()
    => EnemySpawner.Instance.RemoveFromList(this);
}