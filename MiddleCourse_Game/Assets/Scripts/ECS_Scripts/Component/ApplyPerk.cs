using Assets.ECS_2.interfaces;
using UnityEngine;
using System.Collections.Generic;

public class ApplyPerk : MonoBehaviour, IAbilityTarget
{
    public bool perk;

    public List<GameObject> targets { get; set; }

    public void Execute()
    {
        foreach (var target in targets)
        {
            if (target != null && target.GetComponent<MoveAbility>())
            {
                perk = true;
                gameObject.SetActive(false);
                gameObject.transform.position = new Vector3(0, -5, 0);
            }
            else return;     
        }
    }

    public void Stop()
    {

    }
}

