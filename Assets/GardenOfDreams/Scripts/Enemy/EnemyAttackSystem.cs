using Assets.GardenOfDreams.Scripts.Interfaces;
using UnityEngine;

public class EnemyAttackSystem : AttackSystem, IDrop
{
    private Enemy _enemy;

    private void Awake()
    => _enemy = GetComponent<Enemy>();

    private void Update()
    {
        if (Player.Instance.AttackSystem.IsAlive && _enemy.IsTargetSet && _isAbleToAttack &&
            Vector2.Distance(transform.position, _enemy.Target.transform.position) <= AttackRange)
        {
            Attack();
        }
    }

    protected override void AttackModify()
    {
        if (_enemy.Target.TryGetComponent(out PlayerAttackSystem attackSystem))
            attackSystem.OnHit(_damage);
    }

    protected override void Die()
    {
        DropItem();
        base.Die();
    }
    public void DropItem()
    {
        var item = Drop.Instance.DropItem();
        var itemPrefab = Instantiate(item, transform.position, Quaternion.identity);
        itemPrefab.GetComponent<BoxCollider2D>().enabled = true;
    }
}