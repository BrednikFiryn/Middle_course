using UnityEngine.AI;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private MoveAbility enemyTarget;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyTarget = FindObjectOfType<MoveAbility>();
    }

    void Update()
    {
        TargetOfEnemyAttack();
    }

    public void TargetOfEnemyAttack()
    {
        if (agent != null && enemyTarget != null)
        {
            agent.SetDestination(enemyTarget.transform.position);
        }
    }
}
