using Assets.ECS_2.interfaces;
using UnityEngine;
using System.Collections.Generic;

public class ApplyDamage : MonoBehaviour, IAbilityTarget
{
    [SerializeField] private float damage;
    private HealthBar healthBar;
    public List<GameObject> targets { get; set; }

    private void Start()
    {
        healthBar = FindObjectOfType<HealthBar>();

    }

    public void Execute()
    {
       if (healthBar == null) return;
       
       foreach (var target in targets)
        {
            var health = target.GetComponent<PlayerStats>();

            if (target != null && health != null && gameObject.GetComponent<ApplyDamage>())
            {
                if (health._health > 0f)
                {
                    health.Damage(damage);
                    healthBar.HealthCheck();
                    health.SavePlayerData();
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
