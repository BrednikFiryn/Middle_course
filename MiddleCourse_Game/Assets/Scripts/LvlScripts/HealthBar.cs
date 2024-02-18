using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public PlayerStats playerStats;
    [SerializeField] private Image _healthBar;
    private float _health;

    public float health
    {
        get => _health;
        set
        {
            _health = value;
            if (_health <= 0)
            {
                gameObject.SetActive(false);
                _health = 0;
            }
            else if (_health > 1) health = 1;
        }
    }

    private void Start()
    {
        playerStats.LoadPlayerData();
        health = playerStats._health;
        _healthBar.fillAmount = health;
    }

    public void HealthCheck()
    {
        health = playerStats._health;
        _healthBar.fillAmount = health;
    }
}



