using Assets.ECS_2.interfaces;
using UnityEngine;
using System.Collections.Generic;

public class ApplyCollisionWall : MonoBehaviour, IAbilityTarget
{
    public List<GameObject> targets { get; set; }

    public void Execute()
    {
        foreach (var target in targets)
        {
            if (target != null && target.CompareTag("bullet"))
            {
                Debug.Log("Bullet Trigger");
            }

            else return;
        }
    }

    public void Stop()
    {

    }
}
