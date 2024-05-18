using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BlockTrap : MonoBehaviour
{
    [SerializeField] private float blockRight = 0.3f;
    [SerializeField] private float blockLeft = 1;
    [SerializeField] private Vector3 vec1;
    [SerializeField] private Vector3 vec2;
    [SerializeField] private AK.Wwise.Event trapEvent = null;

    private void Start()
    {
        DOTween.Sequence()
            .Append(transform.DOMove(vec1, blockRight).OnComplete(TrapSound))
            .Append(transform.DOMove(vec2, blockLeft))
            .SetLoops(-1);
    }

    public void KillTrap()
    {
        DOTween.Clear();
    }

    private void TrapSound()
    {
       trapEvent.Post(gameObject);
    }
}
