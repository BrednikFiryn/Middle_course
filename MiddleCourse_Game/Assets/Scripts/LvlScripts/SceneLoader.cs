using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
   [SerializeField] private int index;

    public void LoadSceneGame()
    {
        SceneManager.LoadScene(index);
    }
}
