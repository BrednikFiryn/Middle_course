using Assets.ECS_2.interfaces;
using UnityEngine;
using System.Collections.Generic;

public class ApplyDamage : MonoBehaviour, IAbilityTarget
{
    [SerializeField] private float damage;
    public List<GameObject> targets { get; set; }

    public void Execute()
    {
       foreach (var target in targets)
        {
            var health = target.GetComponent<HealthBar>();

            if (target != null && health != null && gameObject.CompareTag("Enemy"))
            {
                if (health.health > 0f)
                {
                    health.health -= damage;
                    Debug.LogError("Attack!!!!");
                    gameObject.transform.position =  new Vector3(0, -10, 0);
                    gameObject.SetActive(false);
                }

                else return;
            }
        }
    }

    public void Stop()
    {

    }
}
