using Assets.GardenOfDreams.Scripts.Interfaces;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public abstract class AttackSystem : MonoBehaviour, IHealth
{

    [SerializeField] private float _maxHealth;

    [SerializeField] protected float _attackRange;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _attackSpeed = 1f;
    protected bool _isAbleToAttack;
    protected float _currentHealth;

    public bool IsAlive => CurrentHealth > 0;
    public float AttackRange => _attackRange;
    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;
    public Action OnHealthChange { get; set; }

    [HideInInspector] public UnityEvent<AttackSystemTypes> OnAttackSystemHandler;

    public enum AttackSystemTypes
    {
        OnHit,
        OnDie,
        OnAttack
    }

    private void OnEnable()
    {
        _currentHealth = MaxHealth;
        _isAbleToAttack = true;
    }

    public void OnHit(float damage)
    {
        if (IsAlive)
        {
            _currentHealth -= damage;
            OnAttackSystemHandler?.Invoke(AttackSystemTypes.OnHit);
            OnHealthChange?.Invoke();
        }

        if (!IsAlive)
            Die();
    }

    virtual protected async void Die()
    {
        OnAttackSystemHandler?.Invoke(AttackSystemTypes.OnDie);
        await Task.Delay((int)(2.5f * 1000));
        if (!Application.isPlaying) return;
        gameObject.SetActive(false);
    }

    virtual protected void Attack()
    {
        if (_isAbleToAttack)
        {
            _isAbleToAttack = false;
            OnAttackSystemHandler?.Invoke(AttackSystemTypes.OnAttack);
            AttackModify();
            AttackCooldown();
        }
    }

    virtual protected void AttackModify() { }

    private async void AttackCooldown()
    {
        await Task.Delay((int)(_attackSpeed * 1000));
        if (!Application.isPlaying) return;
        _isAbleToAttack = true;
    }
}