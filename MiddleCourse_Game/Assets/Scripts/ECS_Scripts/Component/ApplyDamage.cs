using Assets.ECS_2.interfaces;
using UnityEngine;
using System.Collections.Generic;

public class ApplyDamage : MonoBehaviour, IAbilityTarget
{
    [SerializeField] private float damage;
    private PlayerStats playerStats;
    private HealthBar healthBar;
    public List<GameObject> targets { get; set; }

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        healthBar = FindObjectOfType<HealthBar>();

    }

    public void Execute()
    {
       foreach (var target in targets)
        {
            var health = target.GetComponent<PlayerStats>();

            if (target != null && health != null && gameObject.CompareTag("Enemy"))
            {
                if (health._health > 0f)
                {
                    health._health -= damage;
                    Debug.LogError("Attack!!!!");
                    healthBar.HealthCheck();
                    playerStats.SavePlayerData();
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
