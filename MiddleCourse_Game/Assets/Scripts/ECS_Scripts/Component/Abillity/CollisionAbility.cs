using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Tilemaps;
using DefaultNamespace;

/// <summary>
/// Класс CollisionAbility является MonoBehaviour и реализует интерфейсы IConvertGameObjectToEntity и IAbility.
/// Он облегчает обнаружение коллизий и выполнение связанных с коллизиями действий в среде Unity ECS (Entity Component System).
/// </summary>
public class CollisionAbility : MonoBehaviour, IConvertGameObjectToEntity, IAbility
{

    public Collider Collider;

    public List<MonoBehaviour> collisionActions = new List<MonoBehaviour>(); 
    public List<IAbilityTarget> collisionActionsAbiliities = new List<IAbilityTarget>();

    [HideInInspector] public List<Collider> collisions;

    /// <summary>
    /// Перебирает каждое коллизионное действие.
    /// Если действие реализует интерфейс IAbilityTarget, добавляет его в список collisionActionsAbilities.
    /// В противном случае регистрирует ошибку.
    /// </summary>
    private void Start() 
    {
        foreach (var action in collisionActions)
        {
            if (action is IAbilityTarget ability) collisionActionsAbiliities.Add(ability); // Проверка является ли action collisionAbility или нет

            else Debug.LogError("CollisionAction must derive from collisionAbility!");
        }
    }

    /// <summary>
    /// Перебирает каждое действие в collisionActionsAbilities.
    /// Инициализирует цели и добавляет сталкивающиеся игровые объекты в список целей каждого действия.
    /// Выполняет действие.
    /// </summary>
    public void Execute()
    {
        foreach (var action in collisionActionsAbiliities) // вызываем Execute() у какждого action в collisionActionsAbiliities
        {
            action.targets = new List<GameObject>(); // инициализация Targets
            // Доабвляем в список Targets gameObject каждого элемента списка
            collisions.ForEach(_collision =>
            {
                if (_collision != null) action.targets.Add(_collision.gameObject);
            });
            action.Execute();
        }
    }

    /// <summary>
    /// Преобразует локальные координаты коллайдера в глобальные (мир) координаты.
    /// Определяет тип коллайдера(сфера, капсула или параллелепипед) и добавляет соответствующие данные в менеджер сущностей.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="dstManager"></param>
    /// <param name="conversionSystem"></param>
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        //Получаем позицию текущего объекта gameObject в трехмерном пространстве и сохраняем ее в переменной position.
        float3 position = gameObject.transform.position;

        //Начинаем проверку типа коллайдера, используя конструкцию switch. Collider - это переменная, представляющая коллайдер, который мы хотим сконвертировать.
        switch (Collider)
        {
            //Если тип коллайдера совпадает с SphereCollider, выполняем следующий блок кода.
            case SphereCollider sphere:
                //Вызываем метод ToWorldSpaceSphere, чтобы преобразовать локальные координаты сферы в глобальные координаты.
                //Результаты(центр сферы sphereCenter и радиус сферы sphereRadius) сохраняются в локальные переменные.
                sphere.ToWorldSpaceSphere(out var sphereCenter, out var sphereRaduis);
                //Добавляем компонент данных ActorColliderData в менеджер сущностей dstManager для сущности entity
                //Создаем новый экземпляр ActorColliderData с помощью ключевого слова new.
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    //Устанавливаем тип коллайдера в Sphere, преобразуя значение из перечисления ColliderType в тип Tile.ColliderType.
                    ColliderType = (Tile.ColliderType)ColliderType.Sphere,
                    //Устанавливаем центр сферы, вычитая позицию объекта position из глобальных координат центра сферы sphereCenter.
                    SphereCenter = sphereCenter - position,
                    //Устанавливаем радиус сферы.
                    SphereRadius = sphereRaduis,
                    //Устанавливаем флаг initialTakeOff в true.
                    initialTakeOff = true
                });
                break;

            //Если тип коллайдера совпадает с CapsuleCollider, выполняем следующий блок кода.
            case CapsuleCollider capsule:
                //Вызываем метод ToWorldSpaceCapsule, чтобы преобразовать локальные координаты капсулы в глобальные координаты.
                //Результаты(низ капсулы capsuleStart, верх капсулы capsuleEnd и радиус капсулы capsuleRadius) сохраняются в локальные переменные.
                capsule.ToWorldSpaceCapsule(out var capsuleStart, out var capsuleEnd, out var capsuleRadius);
                //Добавляем компонент данных ActorColliderData в менеджер сущностей dstManager для сущности entity
                //Создаем новый экземпляр ActorColliderData с помощью ключевого слова new.
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    //Устанавливаем тип коллайдера в Capsule, преобразуя значение из перечисления ColliderType в тип Tile.ColliderType.
                    ColliderType = (Tile.ColliderType)ColliderType.Capsule,
                    //Устанавливаем низ капсулы, вычитая позицию объекта position из глобальных координат низа капсулы capsuleStart.
                    CapsuleStart = capsuleStart - position,
                    //Устанавливаем верх капсулы, вычитая позицию объекта position из глобальных координат верх капсулы CapsuleEnd.
                    CapsuleEnd = capsuleEnd - position,
                    //Устанавливаем радиус капсулы.
                    CapsuleRadius = capsuleRadius,
                    //Устанавливаем флаг initialTakeOff в true.
                    initialTakeOff = true
                });
                break;

            // //Если тип коллайдера совпадает с BoxCollider, выполняем следующий блок кода.
            case BoxCollider box:
                //Вызываем метод ToWorldSpaceBox, чтобы преобразовать локальные координаты куба в глобальные координаты.
                //Результаты(центр куба boxCenter, содержимое куба boxHalfExtents и ориентация куба boxOrientation) сохраняются в локальные переменные.
                box.ToWorldSpaceBox(out var boxCenter, out var boxHalfExtents, out var boxOrientation);
                //Добавляем компонент данных ActorColliderData в менеджер сущностей dstManager для сущности entity
                //Создаем новый экземпляр ActorColliderData с помощью ключевого слова new.
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    //Устанавливаем тип коллайдера в Box, преобразуя значение из перечисления ColliderType в тип Tile.ColliderType.
                    ColliderType = (Tile.ColliderType)ColliderType.Box,
                    //Устанавливаем центр куба, вычитая позицию объекта position из глобальных координат низа капсулы boxCenter.
                    BoxCenter = boxCenter - position,
                    //Находит содержимое куба.
                    BoxHalfExterns = boxHalfExtents,
                    //Находит ориентацию куба.
                    BoxOrientation = boxOrientation,
                    //Устанавливаем флаг initialTakeOff в true.
                    initialTakeOff = true
                });
                break;
        }

        Collider.enabled = false;
    }
}

/* 
Этот метод выполняет конвертацию различных типов коллайдеров (сфера, капсула, параллелепипед) 
в соответствующие данные сущности ActorColliderData, готовые для использования в Unity ECS.
*/

/// <summary>
/// Представляет данные, описывающие текущий коллайдер.
/// Содержит поля для типа коллайдера, центра, начальной/конечной точек (для капсулы), 
/// половины высоты(для параллелепипеда), радиуса, ориентации(для параллелепипеда) и флага начального взлета.
/// </summary>
public struct ActorColliderData : IComponentData
{
    public Tile.ColliderType ColliderType;
    public float3 SphereCenter;
    public float3 CapsuleStart;
    public float3 CapsuleEnd;
    public float3 BoxCenter;
    public float3 BoxHalfExterns;
    public float SphereRadius;
    public float CapsuleRadius;
    public quaternion BoxOrientation;
    public bool initialTakeOff;
}

/// <summary>
/// Перечисляет различные типы коллайдеров (сфера, капсула и параллелепипед).
/// </summary>
public enum ColliderType
{
    Sphere = 0, 
    Capsule = 1,
    Box = 2
}

/* 
### Техническая документация:

#### Примечания:
- Этот скрипт предназначен для интеграции с Unity ECS и предполагает наличие конкретной функциональности Unity ECS и интерфейсов.
- Он облегчает преобразование игровых объектов с коллайдерами в сущности ECS.
- Предоставляет механизм выполнения действий, связанных с коллизиями, в среде ECS.
- Для правильной инициализации и использования требуется интеграция с рабочими процессами и системами Unity ECS.
*/
