using System.Collections;
using UnityEngine;
// Подключает пространство имен UnityEngine.Networking, которое предоставляет классы для работы с сетью в Unity.
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

/// <summary>
/// Определение класса GoogleDriveLoader, который наследуется от MonoBehaviour и используется для загрузки данных с Google Drive. 
/// </summary>
public class GoogleDriveLoader : MonoBehaviour
{
    // Объявление публичной переменной googleDriveUrl для хранения URL-адреса файла на Google Drive.
    private string googleDriveUrl = "https://drive.google.com/file/d/1qRLxv_Lpy0-_m01KuqOtvtfv6_8dwa-z/view?usp=drive_link";

    // Определение публичного метода SceneReload, который будет вызываться для перезагрузки сцены.
    public void SceneReload()
    {
        // Запуск корутины DriverLoader, которая будет выполняться асинхронно.
        StartCoroutine(DriverLoader());
    }

    // Определение корутины DriverLoader, которая будет выполнять загрузку файла с Google Drive.
    IEnumerator DriverLoader()
    {
        // Создание нового запроса UnityWebRequest для получения данных по указанному URL-адресу.
        UnityWebRequest www = UnityWebRequest.Get(googleDriveUrl);
        // Ожидание завершения запроса и получение результата.
        yield return www.SendWebRequest();

        // Проверка результата выполнения запроса на успешное завершение.
        if (www.result == UnityWebRequest.Result.Success)
        {
            // Получение пути к файлу для сохранения на устройстве.
            string filePath = Application.persistentDataPath + "/player_data.json";
            // Запись данных файла, полученных из Google Drive, в локальный файл.
            System.IO.File.WriteAllText(filePath, www.downloadHandler.text);
        }

        // Получение экземпляра EntityManager для работы с сущностями ECS.
        var entityManager = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager;
        // Уничтожение всех сущностей в текущем мире ECS.
        entityManager.DestroyEntity(entityManager.UniversalQuery);
        // Перезагрузка текущей сцены для обновления загруженных данных
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}


