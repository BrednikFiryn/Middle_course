//using UnityEngine;

//public class WorkingJsonData : MonoBehaviour
//{
//    [SerializeField] private HealthBar _health;

//    private void Start()
//    {
//        LoadPlayerData();
//    }
//    void Update()
//    {
//        SavePlayerData();
//    }

//    void LoadPlayerData()
//    {
//        string filePath = Application.persistentDataPath + "/player_data.json";
//        if (System.IO.File.Exists(filePath))
//        {
//            string jsonData = System.IO.File.ReadAllText(filePath);
//            HealthBar healthBar = JsonUtility.FromJson<HealthBar>(jsonData);
//            _health.health = healthBar.health; // Загружаем здоровье из файла
//            Debug.Log("Loaded player data: " + _health);
//        }
//        else
//        {
//            Debug.LogError("Player data file not found at: " + filePath);
//        }
//    }

//    void SavePlayerData()
//    {
//        string filePath = Application.persistentDataPath + "/player_data.json";
//        HealthBar healthBar = new HealthBar();
//        healthBar.health = _health.health; // Сохраняем текущее здоровье
//        string jsonData = JsonUtility.ToJson(_health.health);
//        System.IO.File.WriteAllText(filePath, jsonData);
//        Debug.Log("Player data saved: " + _health.health);
//    }

//}
