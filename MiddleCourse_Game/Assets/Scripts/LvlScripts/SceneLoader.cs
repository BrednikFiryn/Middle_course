using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
   [SerializeField] private int index;

    public void LoadSceneGame()
    {
        StartCoroutine(LoadSceneCoroutine());
    }

    private IEnumerator LoadSceneCoroutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);
        Cursor.lockState = CursorLockMode.Locked;
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
