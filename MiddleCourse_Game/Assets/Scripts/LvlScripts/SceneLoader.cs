using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
   public void Loader(int index)
    {
        SceneManager.LoadScene(index);
    }
}
