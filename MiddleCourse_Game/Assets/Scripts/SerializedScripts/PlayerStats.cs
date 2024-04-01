using System.Threading.Tasks;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private SettingsWarrior settingsWarrior;
    public float healthHero;

    private async void Awake()
    {
        await LoadPlayerData();
    }

    public async Task LoadPlayerData()
    {
        string filePath = Application.persistentDataPath + "/player_data.json";
        if (System.IO.File.Exists(filePath))
        {
            string jsonData = await System.IO.File.ReadAllTextAsync(filePath);
            if (!string.IsNullOrEmpty(jsonData))
            {
                PlayerData playerData = JsonUtility.FromJson<PlayerData>(jsonData);
                healthHero = playerData.health;
                settingsWarrior.health = playerData.health;
            }
            else
            {
                Debug.LogError("Ошибка загрузки данных: JSON-строка пуста или недопустима.");
            }
        }
        else
        {
            Debug.LogError("Ошибка загрузки данных: файл не найден.");
        }
    }

    public async void SavePlayerData()
    {
        string filePath = Application.persistentDataPath + "/player_data.json";
        PlayerData playerData = new PlayerData(healthHero);
        playerData.health = settingsWarrior.health;
        healthHero = settingsWarrior.health;

        string jsonData = JsonUtility.ToJson(playerData);
        await System.IO.File.WriteAllTextAsync(filePath, jsonData);
    }

    public void Damage(float damage)
    {
        settingsWarrior.health -= damage;

        if (damage >= settingsWarrior.health)
        {
            settingsWarrior.health = 0;
        }
        else return;
    }

    public void Healing(float health)
    {
        settingsWarrior.health += health;
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