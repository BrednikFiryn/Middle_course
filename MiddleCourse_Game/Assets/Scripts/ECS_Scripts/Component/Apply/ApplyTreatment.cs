using DefaultNamespace;
using UnityEngine;
using System.Collections.Generic;

public class ApplyTreatment : MonoBehaviour, IAbilityTarget
{
    [SerializeField] private float treatment;
    private HealthBar _healthBar;
    public List<GameObject> targets { get; set; }

    private void Start()
    {
        _healthBar = FindObjectOfType<HealthBar>();
    }

    public void Execute()
    {
        if (_healthBar == null) return;

        foreach (var target in targets)
        {
            var health = target.GetComponent<PlayerStats>();

            if (target != null && !target.GetComponent<ApplyDamage>() && target.GetComponent<MoveAbility>())
            {
                if (health.healthHero < 1f)
                {
                    health.Healing(treatment);
                    health.SavePlayerData();
                    gameObject.SetActive(false);
                    gameObject.transform.position = new Vector3(0, -15, 0);
                }

                else return;
            }
        }
    }

    public void Stop()
    {

    }
}
