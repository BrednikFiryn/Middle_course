using Assets.ECS_2.interfaces;
using UnityEngine;

public class PullAbility : MonoBehaviour, moveAbility
{
    //public float boostDelay,
    //public float boostSpeed;
    //public Transform LookTransform;
    //private float boostTime = float.MinValue;

    public Animator anim;



    public void Execute()
    {
     //if (Time.time < boostTime + boostDelay) return; // если время еще не прошло то возвращаем действие
     //boostTime = Time.time;
     //Vector3 lookDirection = LookTransform.transform.forward;
     //lookDirection.y = 0;
     //lookDirection.Normalize();
     //Vector3 boostPosition = transform.position + lookDirection * boostSpeed * 1000 * Time.deltaTime;
     //transform.position = boostPosition;
     anim.SetBool("Run", true);
    }

    public void Stop()
    {
        anim.SetBool("Run", false);
    }
}
