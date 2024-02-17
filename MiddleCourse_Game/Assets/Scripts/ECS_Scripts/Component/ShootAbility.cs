using Assets.ECS_2.interfaces;
using UnityEngine;

public class ShootAbility : MonoBehaviour, IAbility
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootDelay;
    [SerializeField] private float bulletSpeed = 100f;

    private float shootTime = float.MinValue;

    public void Execute()
    {
        if (Time.time < shootTime + shootDelay) return; // åñëè âðåìÿ åùå íå ïðîøëî òî âîçâðàùàåì äåéñòâèå

        shootTime = Time.time;

        if (bullet != null)
        {
            var _transform = this.transform;
            bullet.transform.position = _transform.position;
            bullet.SetActive(true);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = _transform.forward * bulletSpeed;
        }

        else Debug.Log("[SHOOT ABILITY] No bullet prefab link!");
    }
}