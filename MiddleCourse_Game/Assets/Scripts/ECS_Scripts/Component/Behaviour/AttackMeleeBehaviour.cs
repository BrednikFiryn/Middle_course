using Assets.ECS_2.interfaces;
using UnityEngine;

public class AttackMeleeBehaviour : MonoBehaviour, IBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _attackTime;
    private HealthBar _healthBar;
    private ApplyDamage _applyDamage;
    private PlayerStats _playerStats;

    private float _attackTimeMin = float.MinValue;

    private void Start()
    {
        _healthBar = FindObjectOfType<HealthBar>();
        _applyDamage = FindObjectOfType<ApplyDamage>();
        _playerStats = FindObjectOfType<PlayerStats>();
    }

    public float Evaluate()
    {

        if (_healthBar == null) return 0;
        return 1 / (this.gameObject.transform.position - _healthBar.transform.position).magnitude;
    }

    public void Behave()
    {
        if (Time.time < _attackTimeMin + _attackTime) return;

        if (_applyDamage.attack)
        {
            AttackMelee();
        }

        else return;
    }

    private void AttackMelee()
    {
        _playerStats.Damage(_damage);
        _healthBar.HealthCheck();
        _playerStats.SavePlayerData();
        _attackTimeMin = Time.time;
    }
}
