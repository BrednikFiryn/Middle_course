using Assets.ECS_2.interfaces;
using UnityEngine;
using UnityEngine.AI;

public class MoveBehaviour : MonoBehaviour, IBehaviour
{
    private MoveAbility _enemyTarget;
    private NavMeshAgent _agent;

    private void Awake()
    {
        gameObject.AddComponent<NavMeshAgent>();
        _agent = GetComponent<NavMeshAgent>();
        _enemyTarget = FindObjectOfType<MoveAbility>();
    }

    public float Evaluate()
    {
        return 0.5f;
    }

    public void Behave()
    {
        if (_agent != null)
        {
            TargetOfEnemyAttack();
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
}
