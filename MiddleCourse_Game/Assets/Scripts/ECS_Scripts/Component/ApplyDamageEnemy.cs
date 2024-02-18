using Assets.ECS_2.interfaces;
using UnityEngine;
using System.Collections.Generic;

public class ApplyDamageEnemy : MonoBehaviour, IAbilityTarget
{
    public List<GameObject> targets { get; set; }

    public void Execute()
    {
        foreach (var target in targets)
        {
            if (target != null && target.GetComponent<ApplyCollisionWall>() && gameObject.GetComponent<ApplyDamageEnemy>())
            {
                gameObject.transform.position = new Vector3(0, -10, 0);
                gameObject.SetActive(false);
            }

            else return;
        }
    }

    public void Stop()
    {

    }
}
