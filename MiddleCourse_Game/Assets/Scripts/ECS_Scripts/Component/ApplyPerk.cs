using Assets.ECS_2.interfaces;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class ApplyPerk : MonoBehaviour, IAbilityTarget
{
    [SerializeField] private float perkTime;
    [SerializeField] private GameObject perkIcon;
    [SerializeField] private Image image;
    private float perkTimeMax = float.MinValue;
    public bool perk;
    public List<GameObject> targets { get; set; }

    private void Update()
    {
        if (perk == true)
        {
            image.fillAmount -= 1.0f / perkTime * Time.deltaTime;
        }
    }

    public void Execute()
    {
        foreach (var target in targets)
        {
            if (target != null && target.CompareTag("Player") && gameObject.CompareTag("Rebound"))
            {
                perk = true;
                image.fillAmount = 1;
                perkIcon.SetActive(true);
                gameObject.transform.position = new Vector3(0, -20, 0);
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
        perkTimeMax = Time.time;
        yield return new WaitForSeconds(perkTime);
        perkIcon.SetActive(false);
        image.fillAmount = 1;
        perk = false;
    }
}

