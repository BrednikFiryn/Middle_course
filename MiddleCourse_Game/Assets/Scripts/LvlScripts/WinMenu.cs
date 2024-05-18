using UnityEngine;

public class WinMenu : MonoBehaviour,IEnemy
{
    [SerializeField] GameObject winPanel;

    private void Start()
    {
        Invoke("Count", 2f);
    }

    private void Count() { IEnemy.EnemyCount = FindObjectsOfType<MoveBehaviour>().Length; }

    public void Win() 
    { 
        winPanel.SetActive(true);
        var entityManager = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager;
        entityManager.DestroyEntity(entityManager.UniversalQuery);
    }
}
