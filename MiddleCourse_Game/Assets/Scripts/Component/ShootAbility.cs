using Assets.ECS_2.interfaces;
using UnityEngine;

public class ShootAbility : MonoBehaviour, IAbility
{
    public GameObject bullet;
    public float shootDelay;
    public Animator anim;

    private float shootTime = float.MinValue;

    public void Execute()
    {

        if (Time.time < shootTime + shootDelay) return; // если время еще не прошло то возвращаем действие

        shootTime = Time.time;
        anim.SetTrigger("RunGun");

        if (bullet != null)
        {
            var t = transform;
            var NewBullet = Instantiate(bullet, t.position, t.rotation);
        }

        else Debug.Log("[SHOOT ABILITY] No bullet prefab link!");
    }
}
