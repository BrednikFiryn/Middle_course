using System;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField] private Settings _settings;
    [SerializeField] private Image _healthBar;
    private float _health = int.MaxValue;

    public ShootAbility shootAbility;

    public float health
    {
        get => _health;
        set
        {
            _health = value;
            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
            WriteStatistics();
        }
    }

    private void WriteStatistics()
    {
        var jsonString = JsonUtility.ToJson(shootAbility.playerStats);
        Debug.Log(jsonString);
        PlayerPrefs.SetString("Stats", jsonString);
    }

    private void Start()
    {
        health = _settings.HeroHealth;
    }

    void Update()
    {
        HealthCheck();
    }

    private void HealthCheck()
    {
        _healthBar.fillAmount = health;  
    }
}
