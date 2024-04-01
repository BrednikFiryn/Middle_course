
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
    private float _health;

    public float health
    {
        get => _health;
        set
        {
            _health = value;
            if (_health <= 0)
            {
                _health = 0;
                _playerStats.EntityDestroy();
            }
            else if (_health > 1) health = 1;
        }
    }

    private void Update()
    {
        HealthCheck();

        if (settingsWarrior.health <= 0)
        {
            _animDeath.SetBool(_deathAnimHash, true);
        }

    }

    private void Start()
    {
        _playerStats = FindObjectOfType<PlayerStats>();
        _animDeath = GetComponent<Animator>();
        HealthStatus();
        health = settingsWarrior.health;
    }

    public void HealthCheck()
    {
         health = settingsWarrior.health;
        _healthCount.fillAmount = health;
    }

    private void HealthStatus()
    {
        _healthBar = GameObject.FindGameObjectWithTag("Health");
        _healthCount = _healthBar.GetComponent<Image>();
    }
}




