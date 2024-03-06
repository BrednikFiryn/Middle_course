using UnityEngine;

public class CollisionShield : MonoBehaviour
{
    [SerializeField] private float _powerBounce = 10;
    private const string enemyTag = "Enemy";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.AddForce(-transform.position * _powerBounce, ForceMode.Impulse);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.AddForce(-transform.position * _powerBounce, ForceMode.Impulse);
        }
    }
}
