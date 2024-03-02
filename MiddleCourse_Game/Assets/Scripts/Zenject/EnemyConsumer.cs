using UnityEngine;
using Zenject;

public class EnemyConsumer : MonoBehaviour
{
    public class Factory : PlaceholderFactory<EnemyConsumer> { }
}
