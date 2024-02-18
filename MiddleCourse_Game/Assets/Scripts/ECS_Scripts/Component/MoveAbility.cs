using Assets.ECS_2.interfaces;
using UnityEngine;

public class MoveAbility : MonoBehaviour, moveAbility
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Execute()
    {  
        anim.SetBool("Walk", true);
    }

    public void Stop()
    {
        anim.SetBool("Walk", false);
    }

}
