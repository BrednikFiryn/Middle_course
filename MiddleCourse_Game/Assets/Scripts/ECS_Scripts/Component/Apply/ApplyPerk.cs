using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ApplyPerk : MonoBehaviour
{
    [SerializeField] private float perkTime;
    public List<GameObject> targets { get; set; }

    public void Execute()
    {
       gameObject.transform.position = new Vector3(0, -20, 0);
       StartCoroutine(EndPerkRoutine());
    }

    public void Stop()
    {

    }

    private IEnumerator EndPerkRoutine()
    {
        yield return new WaitForSeconds(perkTime);
    }
}

