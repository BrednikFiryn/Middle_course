using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private ShootAbility shootAbility;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
            gameObject.transform.position = new Vector3(0, -5, 0);
        }
    }

}
