using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GoogleDriveLoader : MonoBehaviour
{
    public string googleDriveUrl;

    public void SceneReload()
    {
        StartCoroutine(DriverLoader());
    }

    IEnumerator DriverLoader()
    {
        UnityWebRequest www = UnityWebRequest.Get(googleDriveUrl);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to download file from Google Drive: " + www.error);
        }
        else
        {
            string filePath = Application.persistentDataPath + "/player_data.json";
            System.IO.File.WriteAllText(filePath, www.downloadHandler.text);
            Debug.Log("File downloaded and saved to: " + filePath);
        }

        var entityManager = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager;
        entityManager.DestroyEntity(entityManager.UniversalQuery);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}


