using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyMarker[] _spawnPoint;

    [Inject]
    EnemyConsumer.Factory greetingConsumerFactory;

    private void Start()
    {
        _spawnPoint = FindObjectsOfType<EnemyMarker>();

        foreach (EnemyMarker sp in _spawnPoint)
        {
            EnemyConsumer consumer = greetingConsumerFactory.Create();
            consumer.transform.position = sp.transform.position;
        }
    }
}
