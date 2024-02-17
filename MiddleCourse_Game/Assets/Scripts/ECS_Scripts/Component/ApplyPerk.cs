using Assets.ECS_2.interfaces;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class ApplyPerk : MonoBehaviour, IAbilityTarget
{
    [SerializeField] private float perkTime;
    [SerializeField] private GameObject perkIcon;
    public bool perk;
    public List<GameObject> targets { get; set; }

    public void Execute()
    {
        foreach (var target in targets)
        {
            if (target != null && target.CompareTag("Player"))
            {
                perk = true;
                perkIcon.SetActive(true);
                gameObject.transform.position = new Vector3(0, -5, 0);
                StartCoroutine(EndPerkRoutine());
            }
            else return;     
        }
    }

    public void Stop()
    {

    }

    private IEnumerator EndPerkRoutine()
    {
        yield return new WaitForSeconds(perkTime);
        perkIcon.SetActive(false);
        perk = false;
    }
}

