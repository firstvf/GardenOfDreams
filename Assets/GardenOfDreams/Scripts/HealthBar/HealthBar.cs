using Assets.GardenOfDreams.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject _healthBarUI;
    [SerializeField] private Image _fillHealthImage;
    private IHealth _iHealth;

    private void Awake()
    => _iHealth = GetComponentInParent<IHealth>();

    private void OnEnable()
    {
        _iHealth.OnHealthChange += UpdateHealthUI;
        UpdateHealthUI();
    }

    private void Start()
    => UpdateHealthUI();

    private void UpdateHealthUI()
    {
        _fillHealthImage.fillAmount = (float)_iHealth.CurrentHealth / _iHealth.MaxHealth;

        if (_iHealth.CurrentHealth == _iHealth.MaxHealth || _iHealth.CurrentHealth <= 0)
            _healthBarUI.SetActive(false);
        else if (_iHealth.CurrentHealth != _iHealth.MaxHealth && !_healthBarUI.activeInHierarchy)
            _healthBarUI.SetActive(true);
    }

    private void OnDisable()
    => _iHealth.OnHealthChange -= UpdateHealthUI;
}