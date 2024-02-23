using Assets.ECS_2.interfaces;
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
            var health = target.GetComponent<PlayerStats>();

            if (target != null && health != null && gameObject.GetComponent<ApplyDamage>())
            {
                if (health._health > 0f)
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