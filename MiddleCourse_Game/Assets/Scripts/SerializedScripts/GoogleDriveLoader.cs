using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UniRx;
using System;

public class GoogleDriveLoader : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private BlockTrap _blockTrap;

    private string _googleDriveUrl = "https://drive.google.com/file/d/1qRLxv_Lpy0-_m01KuqOtvtfv6_8dwa-z/view?usp=drive_link";

    public void SceneReload()
    {
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
        SceneManager.LoadScene(index);
    }
}


