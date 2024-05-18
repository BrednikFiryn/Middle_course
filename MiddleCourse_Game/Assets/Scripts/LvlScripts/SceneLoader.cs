using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
   [SerializeField] private int index;
    [SerializeField] private GameObject mainCamera;

    public void LoadSceneGame()
    {
        mainCamera.GetComponent<AkEvent>().Stop(0);
        SceneManager.LoadScene(index);
    }
}
