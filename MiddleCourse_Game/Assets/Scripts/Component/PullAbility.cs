using Assets.ECS_2.interfaces;
using UnityEngine;

public class PullAbility : MonoBehaviour, moveAbility
{
    public Animator anim;

    public void Execute()
    {
     anim.SetBool("Run", true);
    }

    public void Stop()
    {
        anim.SetBool("Run", false);
    }
}
