using Assets.ECS_2.interfaces;
using UnityEngine;

public class MoveAbility : MonoBehaviour, moveAbility
{
    public Animator anim;

    public void Execute()
    {  
        anim.SetBool("Walk", true);
    }

    public void Stop()
    {
        anim.SetBool("Walk", false);
    }

}
