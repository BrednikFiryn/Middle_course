using DefaultNamespace;
using UnityEngine;
using System.Collections.Generic;

public class ApplyDamage : MonoBehaviour, IAbilityTarget
{
    public bool attack = true;
    public List<GameObject> targets { get; set; }

    public void Execute()
    {
        foreach (var target in targets)
        {
            var health = target.GetComponent<HealthBar>();

            if (target != null && health != null && gameObject.GetComponent<ApplyDamage>())
            {
                if (health.health > 0f)
                {
                    attack = true;
                }

                else attack = false;                  
            }
        }
    }

    public void Stop()
    {

    }
}