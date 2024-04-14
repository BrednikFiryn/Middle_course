using DefaultNamespace;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class ApplyDamageEnemy : MonoBehaviour, IAbilityTarget
{
    public List<GameObject> targets { get; set; }
    private CharacterData _characterData;

    private void Start()
    {
        _characterData = FindObjectOfType<CharacterData>();
    }

    public void Execute()
    {
        foreach (var target in targets)
        {
            if (target != null && target.GetComponent<ApplyDamage>() && gameObject.GetComponent<ApplyDamageEnemy>())
            {
                target.transform.position = new Vector3(0, -10, 0);
                target.SetActive(false);
                Destroy(target.GetComponent<NavMeshAgent>());
                _characterData.Score(10);
            }
            else return;
        }
    }

    public void Stop()
    {

    }
}
