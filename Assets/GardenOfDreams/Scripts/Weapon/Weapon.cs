using System.Threading.Tasks;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ItemType _weaponType;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _reloadSpeed;
    [SerializeField] private int _magazineSize;

    public ItemType GetWeaponType => _weaponType;
    public float CurrentMagazineSize { get; private set; }
    public float Damage => _damage;
    public float AttackRange => _attackRange;
    public float AttackSpeed => _attackSpeed;
    public float ReloadSpeed => _reloadSpeed;
    public int MagazineSize => _magazineSize;
    public bool IsAbleToShoot => CurrentMagazineSize > 0;

    private void Start()
    {
        CurrentMagazineSize = _magazineSize;
    }

    private void Shoot()
    {
        if (CurrentMagazineSize > 0)
            CurrentMagazineSize--;
    }

    private async void ReloadWeapon()
    {
        await Task.Delay((int)(_reloadSpeed * 1000));
        CurrentMagazineSize = _magazineSize;
    }
}