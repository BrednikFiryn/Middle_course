using Assets.ECS_2.interfaces;
using UnityEngine;
using UnityEngine.AI;

public class MoveBehaviour : MonoBehaviour, IBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private string zombiAnimHash;
    [SerializeField] float zoneAgression;
    [SerializeField] private float damageModifier;
    [SerializeField] private float attackTime;
    private HealthBar _healthBar;
    private ApplyDamage _applyDamage;
    private PlayerStats _playerStats;
    private MoveAbility _enemyTarget;
    private Animator _anim;
    private NavMeshAgent _agent;
    private float _attackTimeMin = float.MinValue;

   [HideInInspector] public float damage;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _enemyTarget = FindObjectOfType<MoveAbility>();
        _agent.speed = speed;
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        _healthBar = FindObjectOfType<HealthBar>();
        _applyDamage = GetComponent<ApplyDamage>();
        _playerStats = FindObjectOfType<PlayerStats>();
    }

    public float Evaluate()
    {
        return zoneAgression;
    }

    public void Behave()
    {
        if (_agent != null)
        {
            TargetOfEnemyAttack();

            if (Time.time < _attackTimeMin + attackTime) return;

            if (_applyDamage.attack)
            {
                AttackMelee();
            }
        }
        else return;
    }

    public void TargetOfEnemyAttack()
    {
        if (_agent != null && _enemyTarget != null)
        {
            _agent.SetDestination(_enemyTarget.transform.position);
        }
    }

    private void AttackMelee()
    {
        damage = IBehaviour.damage * damageModifier;
        Debug.Log(damage);
        _playerStats.Damage(damage);
        _healthBar.HealthCheck();
        _attackTimeMin = Time.time;
    }
}
