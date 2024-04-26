using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UniRx;

public class GoogleDriveLoader : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private BlockTrap _blockTrap;

    private string _googleDriveUrl = "https://drive.google.com/uc?export=download&id=1fJMNT3BMWyntPrd9jfgwROw0VwdAGrvS";

    public void SceneReload()
    {
        Cursor.lockState = CursorLockMode.Locked;
       Observable.FromCoroutine(_ => DriverLoader())
            .Subscribe(_ => Debug.Log("Загрузка завершена"),
            ex => Debug.LogError($"Ошибка: {ex.Message}"));
    }

    IEnumerator DriverLoader()
    {
        UnityWebRequest www = UnityWebRequest.Get(_googleDriveUrl);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string filePath = Application.persistentDataPath + "/player_data.json";
            System.IO.File.WriteAllText(filePath, www.downloadHandler.text);
        }

        else Debug.LogError("Нет подключения к интернету");

        _blockTrap.KillTrap();
        var entityManager = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager;
        entityManager.DestroyEntity(entityManager.UniversalQuery);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);
        yield return asyncLoad;
    }
}


