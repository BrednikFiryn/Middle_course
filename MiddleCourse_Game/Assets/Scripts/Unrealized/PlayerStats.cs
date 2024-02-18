using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float _health;

    /// <summary>
    /// Метод для загрузки данных игрока из файла JSON.
    /// </summary>
    public void LoadPlayerData()
    {
        // Путь к файлу JSON в системной папке приложения.
        string filePath = Application.persistentDataPath + "/player_data.json";
        // Проверка существования файла JSON.
        if (System.IO.File.Exists(filePath))
        {
            // Чтение содержимого файла JSON.
            string jsonData = System.IO.File.ReadAllText(filePath);
            // Десериализация данных из JSON в объект PlayerData.
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(jsonData);
            // Загрузка значения здоровья из объекта PlayerData.
            _health = playerData.health;
        }

        else return;
    }

    /// <summary>
    ///  Метод для сохранения данных игрока в файл JSON.
    /// </summary>
    public void SavePlayerData()
    {
        // Путь к файлу JSON в системной папке приложения.
        string filePath = Application.persistentDataPath + "/player_data.json";
        // Создание нового объекта PlayerData с текущим значением здоровья.
        PlayerData playerData = new PlayerData(_health);
        // Присваивание текущего значения здоровья объекту PlayerData.
        playerData.health = _health;
        // Преобразование объекта PlayerData в JSON строку.
        string jsonData = JsonUtility.ToJson(playerData);
        // Запись JSON строки в файл.
        System.IO.File.WriteAllText(filePath, jsonData);
    }

    public void Damage(float damage)
    {
        _health -= damage;
    }

    public void Healing(float health)
    {
        _health += health;
    }
}

//Атрибут, указывающий, что класс PlayerData может быть сериализован в JSON.
[System.Serializable]
public class PlayerData
{
    // Переменная, представляющая здоровье игрока.
    public float health;

    // Конструктор класса PlayerData для установки начального значения здоровья.
    public PlayerData(float health)
    {
        this.health = health;
    }
}