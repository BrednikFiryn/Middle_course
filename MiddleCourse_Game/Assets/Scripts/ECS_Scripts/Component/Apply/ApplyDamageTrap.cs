using DefaultNamespace;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class ApplyDamageTrap : MonoBehaviour, IAbilityTarget
{
    public List<GameObject> targets { get; set; }
    [SerializeField] private float _damage;
    private HealthBar _healthBar;
    private PlayerStats _playerStats;

    public void Execute()
    {
        foreach (var target in targets)
        {
            _healthBar = target?.gameObject?.GetComponent<HealthBar>();
            _playerStats = target?.gameObject?.GetComponent<PlayerStats>();

            if (_healthBar != null)
            {         
               DamageTrap();
            }
        }
    }

    public void Stop()
    {

    }


    private void DamageTrap()
    {
        _playerStats.Damage(_damage);
        _healthBar.HealthCheck();
        _playerStats.SavePlayerData();
    }
}