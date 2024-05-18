using DefaultNamespace;
using UnityEngine;

public class MoveAbility : MonoBehaviour, moveAbility
{
    [SerializeField] private float walkDelay = 1f;
    private float _walkTime = float.MinValue;

    public void Execute(float speed)
    {
        walkDelay = speed;
        if (Time.time < _walkTime + walkDelay) return;
        _walkTime = Time.time;
        AkSoundEngine.PostEvent("Walk", gameObject);
    }

    public void Stop()
    {

    }
}
