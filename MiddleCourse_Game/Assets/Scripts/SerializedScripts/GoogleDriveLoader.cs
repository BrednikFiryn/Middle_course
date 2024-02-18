using System.Collections;
using UnityEngine;
// ���������� ������������ ���� UnityEngine.Networking, ������� ������������� ������ ��� ������ � ����� � Unity.
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

/// <summary>
/// ����������� ������ GoogleDriveLoader, ������� ����������� �� MonoBehaviour � ������������ ��� �������� ������ � Google Drive. 
/// </summary>
public class GoogleDriveLoader : MonoBehaviour
{
    // ���������� ��������� ���������� googleDriveUrl ��� �������� URL-������ ����� �� Google Drive.
    private string googleDriveUrl = "https://drive.google.com/file/d/1qRLxv_Lpy0-_m01KuqOtvtfv6_8dwa-z/view?usp=drive_link";

    // ����������� ���������� ������ SceneReload, ������� ����� ���������� ��� ������������ �����.
    public void SceneReload()
    {
        // ������ �������� DriverLoader, ������� ����� ����������� ����������.
        StartCoroutine(DriverLoader());
    }

    // ����������� �������� DriverLoader, ������� ����� ��������� �������� ����� � Google Drive.
    IEnumerator DriverLoader()
    {
        // �������� ������ ������� UnityWebRequest ��� ��������� ������ �� ���������� URL-������.
        UnityWebRequest www = UnityWebRequest.Get(googleDriveUrl);
        // �������� ���������� ������� � ��������� ����������.
        yield return www.SendWebRequest();

        // �������� ���������� ���������� ������� �� �������� ����������.
        if (www.result == UnityWebRequest.Result.Success)
        {
            // ��������� ���� � ����� ��� ���������� �� ����������.
            string filePath = Application.persistentDataPath + "/player_data.json";
            // ������ ������ �����, ���������� �� Google Drive, � ��������� ����.
            System.IO.File.WriteAllText(filePath, www.downloadHandler.text);
        }

        // ��������� ���������� EntityManager ��� ������ � ���������� ECS.
        var entityManager = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager;
        // ����������� ���� ��������� � ������� ���� ECS.
        entityManager.DestroyEntity(entityManager.UniversalQuery);
        // ������������ ������� ����� ��� ���������� ����������� ������
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}


