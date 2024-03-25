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
    [SerializeField] private int index;
    [SerializeField] private BlockTrap _blockTrap;
    // ���������� ��������� ���������� googleDriveUrl ��� �������� URL-������ ����� �� Google Drive.
    private string _googleDriveUrl = "https://drive.google.com/file/d/1qRLxv_Lpy0-_m01KuqOtvtfv6_8dwa-z/view?usp=drive_link";

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
        UnityWebRequest www = UnityWebRequest.Get(_googleDriveUrl);
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

        else Debug.LogError("��� ����������� � ���������");

        _blockTrap.KillTrap();
        // ��������� ���������� EntityManager ��� ������ � ���������� ECS.
        var entityManager = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager;
        // ����������� ���� ��������� � ������� ���� ECS.
        entityManager.DestroyEntity(entityManager.UniversalQuery);
        SceneManager.LoadScene(index);
    }
}


