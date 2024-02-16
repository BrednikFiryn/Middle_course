using Assets.ECS_2.interfaces;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Класс наследует от MonoBehaviour и реализует интерфейс IConvertGameObjectToEntity, что позволяет ему быть преобразованным в сущность Unity.
/// </summary>
public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity 
{
     public float speed;
     public float health;

     public MonoBehaviour ShootAction;
     public MonoBehaviour MoveAction;
     public MonoBehaviour BoostAction;

    /// <summary>
    /// Метод, который вызывается при преобразовании объекта в сущность. Он добавляет компоненты данных в сущность на основе параметров объекта и его действий.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="dstManager"></param>
    /// <param name="conversionSystem"></param>
    /// Convert: Принимает сущность, менеджер сущностей и систему преобразования игрового объекта.
    public void Convert(Entity entity, EntityManager dstManager,
        GameObjectConversionSystem conversionSystem)
    {
        // AddComponentData: Добавляет компоненты данных в сущность, такие как InputData, ShootData и InterfaceData.
        dstManager.AddComponentData(entity, new InputData());
        dstManager.AddComponentData(entity, new InterfaceData { Health = health });

        // Проверяет, заполнена ли переменная ShootAction и является ли она наследником интерфейса IAbility,
        // и, если это так, добавляет компонент ShootData.
        if (ShootAction != null && ShootAction is IAbility)
        {
            dstManager.AddComponentData(entity, new ShootData());
        }
    }
}

/// <summary>
/// Определяет данные ввода, такие как вектор движения, значение стрельбы, значение ускорения и скорость.
/// </summary>
public struct InputData : IComponentData
{
    public float2 move;
    public float shoot;
    public float boost;
    public float Speed;
}

/// <summary>
/// Представляет данные, связанные со стрельбой, которые могут быть добавлены к сущности, чтобы обозначить, что объект может стрелять.
/// </summary>
public struct ShootData : IComponentData 
{

}

/// <summary>
/// Содержит информацию о здоровье объекта.
/// </summary>
public struct InterfaceData : IComponentData
{
    public float Health;
}

/* 
### Техническая документация:

#### 1. Назначение:
Этот код определяет класс UserInputData, который используется для управления вводом и хранения данных игрового объекта.

#### 2. Ключевые особенности:
- Переменные speed, health и boost определяют основные характеристики объекта.
- Convert метод используется для преобразования объекта в сущность Unity и добавления соответствующих компонент данных.
- InputData, ShootData и InterfaceData определяют структуры данных для хранения важной информации.

#### 3. Использование:
Этот класс может быть применен к игровым объектам, чтобы управлять их поведением на основе пользовательского ввода и других параметров.
*/