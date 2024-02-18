using Assets.ECS_2.interfaces;
using UnityEngine;
using System.Collections.Generic;

public class ApplyTreatment : MonoBehaviour, IAbilityTarget
{
    [SerializeField] private float treatment;
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

            if (target != null && health != null)
            {
                if (health._health < 1f)
                {
                    health._health += treatment;
                    healthBar.HealthCheck();
                    playerStats.SavePlayerData();
                    gameObject.SetActive(false);
                    gameObject.transform.position = new Vector3(0, -15, 0);
                }

                else return;
            }
        }
    }

    public void Stop()
    {

    }
}
