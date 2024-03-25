using DG.Tweening;
using UnityEngine;

public class BlockTrap : MonoBehaviour
{
    [SerializeField] private float _blockRight = 0.3f;
    [SerializeField] private float _blockLeft = 1;
    [SerializeField] private Vector3 vec1;
    [SerializeField] private Vector3 vec2;

    private void Start()
    {
        DOTween.Sequence()
            .Append(transform.DOMove(vec1, _blockRight))
            .Append(transform.DOMove(vec2, _blockLeft))
            .SetLoops(-1);      
    }

    public void KillTrap()
    {
        DOTween.Clear();
    }
}
