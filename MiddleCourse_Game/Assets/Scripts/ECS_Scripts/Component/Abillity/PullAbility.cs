using DefaultNamespace;
using UnityEngine;

public class PullAbility : MonoBehaviour, moveAbility
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Execute()
    {
     anim.SetBool("Run", true);
    }

    public void Stop()
    {
        anim.SetBool("Run", false);
    }
}
