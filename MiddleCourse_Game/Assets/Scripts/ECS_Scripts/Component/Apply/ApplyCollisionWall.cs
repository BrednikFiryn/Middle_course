using DefaultNamespace;
using UnityEngine;
using System.Collections.Generic;

public class ApplyCollisionWall : MonoBehaviour, IAbilityTarget
{
    public List<GameObject> targets { get; set; }

    public void Execute()
    {
        foreach (var target in targets)
        {
            if (target != null && target.CompareTag("Wall"))
            {
                gameObject.SetActive(false);
                gameObject.transform.position = new Vector3(0, -15, 0);
            }
            else return;
        }
    }

    public void Stop()
    {

    }
}
