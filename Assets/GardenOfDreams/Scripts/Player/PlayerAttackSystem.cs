using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem : AttackSystem
{
    private List<Enemy> _enemyList;

    private void Start()
    => _enemyList = EnemySpawner.Instance.EnemyList;

    public void SetWeapon(Weapon weapon)
    {
        _attackRange = weapon.AttackRange;
        _damage = weapon.Damage;
        _attackSpeed = weapon.AttackSpeed;
        Debug.Log($"Set weapon: [{weapon.GetWeaponType}]");
    }

    public void Shoot()
    {
        foreach (var enemy in _enemyList)
            if (Vector2.Distance(transform.position, enemy.transform.position) <= AttackRange)
                Attack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }

    protected override void Attack()
    {
        base.Attack();
    }

    protected override void AttackModify()
    {
        foreach (var enemy in _enemyList)
        {
            if (Vector2.Distance(transform.position, enemy.transform.position) <= AttackRange && enemy.AttackSystem.IsAlive)
            {
                if (Player.Instance.Inventory.TryGetAmmo())
                {
                    enemy.AttackSystem.OnHit(_damage);
                }
                else
                {
                    Debug.Log("No ammo");
                    return;
                }
                return;
            }
        }
    }
}