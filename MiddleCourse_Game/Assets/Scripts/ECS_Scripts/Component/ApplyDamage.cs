using Assets.ECS_2.interfaces;
using UnityEngine;
using System.Collections.Generic;

public class ApplyDamage : MonoBehaviour, IAbilityTarget
{
    [SerializeField] private float damage;
    public List<GameObject> targets { get; set; }

    public void Execute()
    {
       foreach (var target in targets)
        {
            var health = target.GetComponent<UserInputData>();

            if (target != null && health != null)
            {
                if (health.health > 0f)
                {
                    health.health -= damage;
                    gameObject.transform.position =  new Vector3(0, -5, 0);
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
