using Assets.ECS_2.interfaces;
using UnityEngine;
using System.Collections.Generic;

public class ApplyTreatment : MonoBehaviour, IAbilityTarget
{
    [SerializeField] private float treatment;
    public List<GameObject> targets { get; set; }

    public void Execute()
    {
        foreach (var target in targets)
        {
            var health = target.GetComponent<HealthBar>();

            if (target != null && health != null && gameObject.CompareTag("Health"))
            {
                if (health.health > 0f)
                {
                    health.health += treatment;
                    gameObject.SetActive(false);
                    gameObject.transform.position = new Vector3(0, -5, 0);
                }

                else return;
            }
        }
    }

    public void Stop()
    {

    }
}
