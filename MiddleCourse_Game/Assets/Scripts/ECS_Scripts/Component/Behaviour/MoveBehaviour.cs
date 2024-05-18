using Assets.ECS_2.interfaces;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MoveBehaviour : MonoBehaviour, IBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private string zombiAnimHash;
    [SerializeField] private float zoneAgression;
    [SerializeField] private float damageModifier;
    [SerializeField] private float attackTime;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private ApplyDamage applyDamage;
    [SerializeField] private float walkDelay = 1f;
    private float _walkTime = float.MinValue;
    private HealthBar _healthBar;
    private PlayerStats _playerStats;
    private MoveAbility _enemyTarget;
    private float _attackTimeMin = float.MinValue;

   [HideInInspector] public float damage;
   public AK.Wwise.Event deathEnemyEvent = null;
   public AK.Wwise.Event walkEnemyEvent = null;
    private void Start()
    {
        _enemyTarget = FindObjectOfType<MoveAbility>();
        agent.speed = speed;
        _healthBar = FindObjectOfType<HealthBar>();
        _playerStats = FindObjectOfType<PlayerStats>();
    }

    public float Evaluate()
    {
        return zoneAgression;
    }

    public void Behave()
    {
        if (agent != null)
        {
            TargetOfEnemyAttack();

            if (Time.time < _attackTimeMin + attackTime) return;

            if (applyDamage.attack)
            {
                AttackMelee();
                //Debug.Log($"Attack {gameObject.name}");
            }

            if (Time.time < _walkTime + walkDelay) return;
            _walkTime = Time.time;
            walkEnemyEvent.Post(gameObject);
        }
        else return;
    }

    public void TargetOfEnemyAttack()
    {
        if (agent != null && _enemyTarget != null)
        {
            agent.SetDestination(_enemyTarget.transform.position);
        }
    }

    private void AttackMelee()
    {
        damage = IBehaviour.damage * damageModifier;
        _playerStats.Damage(damage);
        _healthBar.HealthCheck();
        _attackTimeMin = Time.time;
    }
}
