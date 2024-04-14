using UnityEngine;

public class ApplyDamage : MonoBehaviour
{
    public bool attack;

    private void Start()
    {
       attack = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attack = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attack = false;
        }
    }
}