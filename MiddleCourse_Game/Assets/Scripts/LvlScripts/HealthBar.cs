using System;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField] private string deathAnimHash;
    [SerializeField] private SettingsWarrior settingsWarrior;
    [SerializeField] private Animator animDeath;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private AK.Wwise.Event deathEvent = null;
    private GameObject _healthBar;
    private Image _healthCount;
    private ViewModel _viewModel;
    private DeathMenu _deathMenu;
    public float health;

    private void Start()
    {
        _deathMenu = FindObjectOfType<DeathMenu>();
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
            animDeath.SetBool(deathAnimHash, true);
            deathEvent.Post(gameObject);
            playerStats.EntityDestroy();
            _deathMenu.GameOver();

        }
        else if (health > 1) health = 1;
    }

    public void Healing(float health)
    {
        settingsWarrior.health += health;
        if (settingsWarrior.health > 1) settingsWarrior.health = 1;
    }
}





