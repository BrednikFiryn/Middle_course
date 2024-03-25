using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField] private string _deathAnimHash;
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
                _animDeath.SetBool(_deathAnimHash, true);
                _health = 0;
                _playerStats.EntityDestroy();
            }
            else if (_health > 1) health = 1;
        }
    }

    private void Start()
    {
        _animDeath = GetComponent<Animator>();
        HealthStatus();
        HealthCheck();
    }

    public void HealthCheck()
    {
         health = _playerStats._health;
        _healthCount.fillAmount = health;
    }

    private void HealthStatus()
    {
        _playerStats = FindObjectOfType<PlayerStats>();
        _healthBar = GameObject.FindGameObjectWithTag("Health");
        _healthCount = _healthBar.GetComponent<Image>();
        _playerStats.LoadPlayerData();
    }
}



