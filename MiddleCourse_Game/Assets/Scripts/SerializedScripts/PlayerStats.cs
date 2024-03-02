using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float _health;

    /// <summary>
    /// ����� ��� �������� ������ ������ �� ����� JSON.
    /// </summary>
    public void LoadPlayerData()
    {
        // ���� � ����� JSON � ��������� ����� ����������.
        string filePath = Application.persistentDataPath + "/player_data.json";
        // �������� ������������� ����� JSON.
        if (System.IO.File.Exists(filePath))
        {
            // ������ ����������� ����� JSON.
            string jsonData = System.IO.File.ReadAllText(filePath);
            // �������������� ������ �� JSON � ������ PlayerData.
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(jsonData);
            // �������� �������� �������� �� ������� PlayerData.
            _health = playerData.health;
        }
        else return;
    }

    /// <summary>
    ///  ����� ��� ���������� ������ ������ � ���� JSON.
    /// </summary>
    public void SavePlayerData()
    {
        // ���� � ����� JSON � ��������� ����� ����������.
        string filePath = Application.persistentDataPath + "/player_data.json";
        // �������� ������ ������� PlayerData � ������� ��������� ��������.
        PlayerData playerData = new PlayerData(_health);
        // ������������ �������� �������� �������� ������� PlayerData.
        playerData.health = _health;
        // �������������� ������� PlayerData � JSON ������.
        string jsonData = JsonUtility.ToJson(playerData);
        // ������ JSON ������ � ����.
        System.IO.File.WriteAllText(filePath, jsonData);
    }

    public void Damage(float damage)
    {
        _health -= damage;

        if (damage >= _health)
        {
            _health = 0;
        }
        else return;
    }

    public void Healing(float health)
    {
        _health += health;
    }

    public void EntityDestroy()
    {
        var entityManager = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager;
        entityManager.DestroyEntity(entityManager.UniversalQuery);
    }
}

//�������, �����������, ��� ����� PlayerData ����� ���� ������������ � JSON.
[System.Serializable]
public class PlayerData
{
    // ����������, �������������� �������� ������.
    public float health;

    // ����������� ������ PlayerData ��� ��������� ���������� �������� ��������.
    public PlayerData(float health)
    {
        this.health = health;
    }
}