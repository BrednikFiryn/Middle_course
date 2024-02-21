using Assets.ECS_2.interfaces;
using UnityEngine;
using System.Collections.Generic;

public class ApplyCollisionWall : MonoBehaviour, IAbilityTarget
{
    [SerializeField] private ApplyPerk applyPerk;
    private float powerBounce = 10;
    private float minAngle = 45f;
    private float maxAngle = 135f;

    public List<GameObject> targets { get; set; }

    public void Execute()
    {
        foreach (var target in targets)
        {
            if (target != null && target.CompareTag("Wall"))
            {
                //���� ��������� applyPerk ���������, ������� ������ (����) ���������� ���������� � ������������ � ����� �������.
                if (!applyPerk.perk)
                { 
                    gameObject.SetActive(false);
                    gameObject.transform.position = new Vector3(0, -6, 0);
                }

                //���� ��������� applyPerk �������, �������� ������� (����) �������������� ���� � ��������� ����������� � �������� ��������� �����.
                if (applyPerk.perk)
                {            
                    Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                    float randomAngle = Random.Range(minAngle, maxAngle);
                    Vector3 randomDirection = Quaternion.Euler(0, randomAngle, 0) * Vector3.forward;
                    rb.AddForce(randomDirection * powerBounce, ForceMode.Impulse);
                }
            }

            else return;
        }
    }

    public void Stop()
    {

    }
}
