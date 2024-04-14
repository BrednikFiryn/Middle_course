
using System;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField] private string _deathAnimHash;
    [SerializeField] private SettingsWarrior settingsWarrior;
    private GameObject _healthBar;
    private PlayerStats _playerStats;
    private Image _healthCount;
    private Animator _animDeath;
    private ViewModel _viewModel;
    public float _health;

    private void Start()
    {

        _playerStats = FindObjectOfType<PlayerStats>();
        _animDeath = GetComponent<Animator>();
        _viewModel = FindObjectOfType<ViewModel>();
        HealthStatus();
        Invoke("HealthCheck", 1);
    }

    private void HealthStatus()
    {
        _healthBar = GameObject.FindGameObjectWithTag("Health");
        _healthCount = _healthBar.GetComponent<Image>();
    }

    public void HealthCheck()
    {
        _health = settingsWarrior.health;
        _healthCount.fillAmount = _health;

        if (_viewModel != null) _viewModel.Health = Math.Truncate(_health * 100).ToString();

        if (_health <= 0)
        {
            _health = 0;
            _animDeath.SetBool(_deathAnimHash, true);
            _playerStats.EntityDestroy();         
        }
        else if (_health > 1) _health = 1;
    }

    public void Healing(float health)
    {
        settingsWarrior.health += health;
        if (settingsWarrior.health > 1) settingsWarrior.health = 1;
    }

}





