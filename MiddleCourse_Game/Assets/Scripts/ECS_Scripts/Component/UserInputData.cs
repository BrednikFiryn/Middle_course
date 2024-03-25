using DefaultNamespace;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Класс наследует от MonoBehaviour и реализует интерфейс IConvertGameObjectToEntity, что позволяет ему быть преобразованным в сущность Unity.
/// </summary>
public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity 
{
     public float speed;
     public string moveAnimHash;
     public string moveSpeedAnimHash;
     public string boostAnimHash;
     public MonoBehaviour shootAction;
     public MonoBehaviour rotationAction;
     public MonoBehaviour moveAction;
     public MonoBehaviour boostAction;

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
        // AddComponentData: Добавляет компоненты данных в сущность, такие как InputData, ShootData.
        dstManager.AddComponentData(entity, new InputData());

        // Проверяет, заполнена ли переменная ShootAction и является ли она наследником интерфейса IAbility,
        // и, если это так, добавляет компонент ShootData.
        if (shootAction != null && shootAction is IAbility)
        {
            dstManager.AddComponentData(entity, new ShootData());
        }

        if (moveAnimHash != string.Empty)
        {
            dstManager.AddComponentData(entity, new AnimData());
        }
    }
}

/// <summary>
/// Определяет данные ввода, такие как вектор движения, значение стрельбы, значение ускорения и скорость.
/// </summary>
public struct InputData : IComponentData
{
    public float2 move;
    public float2 rotation;
    public float boost;
    public float Speed;
}

/// <summary>
/// Представляет данные, связанные со стрельбой, которые могут быть добавлены к сущности, чтобы обозначить, что объект может стрелять.
/// </summary>
public struct ShootData : IComponentData 
{
    public float shoot;
}

/// <summary>
/// Представляет данные, связанные с анимацией.
/// </summary>
public struct AnimData : IComponentData
{

}