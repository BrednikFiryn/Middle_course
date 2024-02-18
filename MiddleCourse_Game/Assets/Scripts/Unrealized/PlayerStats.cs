using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float _health;

    public void LoadPlayerData()
    {
        string filePath = Application.persistentDataPath + "/player_data.json";
        if (System.IO.File.Exists(filePath))
        {
            string jsonData = System.IO.File.ReadAllText(filePath);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(jsonData);
            _health = playerData.health; // Загружаем здоровье из файла
            Debug.Log("Loaded player data: " + _health);
        }

        else
        {
            Debug.LogError("Player data file not found at: " + filePath);
        }
    }

    public void SavePlayerData()
    {
        string filePath = Application.persistentDataPath + "/player_data.json";
        PlayerData playerData = new PlayerData(_health);
        playerData.health = _health; // Сохраняем текущее здоровье
        string jsonData = JsonUtility.ToJson(playerData);
        System.IO.File.WriteAllText(filePath, jsonData);
        Debug.Log("Player data saved: " + _health);
    }
}

[System.Serializable]
public class PlayerData
{
    public float health;

    public PlayerData(float health)
    {
        this.health = health;
    }
}