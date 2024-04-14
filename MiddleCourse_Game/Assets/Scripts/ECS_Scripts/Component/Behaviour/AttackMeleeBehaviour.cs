using Assets.ECS_2.interfaces;
using UnityEngine;

public class AttackMeleeBehaviour : MonoBehaviour, IBehaviour
{
    private HealthBar _healthBar;

    private void Start()
    {
        _healthBar = FindObjectOfType<HealthBar>();
    }

    public float Evaluate()
    {
        return (this.gameObject.transform.position - _healthBar.transform.position).magnitude;
    }

    public void Behave()
    {

    }
}