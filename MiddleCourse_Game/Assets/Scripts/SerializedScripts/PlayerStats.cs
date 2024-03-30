using UnityEngine;
using System.Threading.Tasks;

public class PlayerStats : MonoBehaviour
{
    public float _health;

    /// <summary>
    /// Метод для загрузки данных игрока из файла JSON.
    /// </summary>
    public async void LoadPlayerData()
    {
        string filePath = Application.persistentDataPath + "/player_data.json";
        if (System.IO.File.Exists(filePath))
        {
            string jsonData = await System.IO.File.ReadAllTextAsync(filePath);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(jsonData);
            _health = playerData.health;
        }
        else return;
    }

    /// <summary>
    ///  Метод для сохранения данных игрока в файл JSON.
    /// </summary>
    public async void SavePlayerData()
    {
        string filePath = Application.persistentDataPath + "/player_data.json";
        PlayerData playerData = new PlayerData(_health);
        playerData.health = _health;
        string jsonData = JsonUtility.ToJson(playerData);
        await System.IO.File.WriteAllTextAsync(filePath, jsonData);
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

[System.Serializable]
public class PlayerData
{
    public float health;
    public PlayerData(float health)
    {
        this.health = health;
    }
}