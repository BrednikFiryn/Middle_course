using UnityEngine;

public class EnemyMarker : MonoBehaviour
{
    //public EnemyType enemyType;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1);
        Gizmos.color = Color.white;
    }
}
