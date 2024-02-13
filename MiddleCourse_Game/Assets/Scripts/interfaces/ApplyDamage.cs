using Assets.ECS_2.interfaces;
using UnityEngine;
using System.Collections.Generic;

public class ApplyDamage : MonoBehaviour, IAbilityTarget
{
    public int Damage;
     public List<GameObject> Targets { get; set; }

    public void Execute()
    {
       foreach (var target in Targets)
        {
            var health = target.GetComponent<UserInputData>();
            if (health != null) health.health -= Damage;
        }
    }

    public void Stop()
    {

    }
}
