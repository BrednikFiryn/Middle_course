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
    public float health;

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
        health = settingsWarrior.health;
        _healthCount.fillAmount = health;

        if (_viewModel != null) _viewModel.Health = 
                Math.Truncate(health * 100).ToString() + $"/{settingsWarrior.maxHealth * 100}";

        if (health <= 0)
        {
            health = 0;
            _animDeath.SetBool(_deathAnimHash, true);
            _playerStats.EntityDestroy();         
        }
        else if (health > 1) health = 1;
    }

    public void Healing(float health)
    {
        settingsWarrior.health += health;
        if (settingsWarrior.health > 1) settingsWarrior.health = 1;
    }
}





