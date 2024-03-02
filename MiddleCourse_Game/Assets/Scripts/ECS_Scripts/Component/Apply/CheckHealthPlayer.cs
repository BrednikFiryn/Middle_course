using DefaultNamespace;
using UnityEngine;
using System.Collections.Generic;

public class CheckHealthPlayer : MonoBehaviour, IAbilityTarget
{
    public List<GameObject> targets { get; set; }

    public void Execute()
    {
        foreach (var target in targets)
        {
            var health = target.GetComponent<HealthBar>();

            if (target != null && health != null && gameObject.GetComponent<HealthBar>())
            {
                health.HealthCheck();
                Debug.Log(health);
            }

            else return;
        }
    }

    public void Stop()
    {

    }
}
