using Assets.ECS_2.interfaces;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

/// <summary>
/// Класс наследует от ComponentSystem, что позволяет ему обрабатывать сущности Unity в системной манере.
/// </summary>
public class AIEvaluateSystem : ComponentSystem
{
    //Объявление переменной moveQuery для запроса сущностей.
    private EntityQuery _evaluateQuery;

    /// <summary>
    /// Вызывается при создании системы и устанавливает запрос для получения сущностей.
    /// </summary>
    protected override void OnCreate()
    {
        // Инициализация переменной moveQuery запросом сущностей, содержащих компонент AIAgent;
        _evaluateQuery = GetEntityQuery(ComponentType.ReadOnly<AIAgent>());
    }

    /// <summary>
    /// Вызывается каждый кадр для обновления данных в компонентах сущностей. Обрабатывает логику перемещения персонажа.
    /// </summary>
    protected override void OnUpdate()
    {
        //- Цикл Entities.With(_evaluateQuery).ForEach перебирает все сущности, удовлетворяющие запросу.
        //-Внутри цикла данные из компонентов сущностей используются для определения направления движения и перемещения персонажа.
        Entities.With(_evaluateQuery).ForEach(
        //Деструктуризация параметров для доступа к компонентам сущности manager.
        (Entity entity, BehaviourManager manager) =>
        {
            IBehaviour bestBehaviour;
            float hightScore = float.MinValue;

            foreach (var behaviour in manager._behaviours)
            {
                if (behaviour is IBehaviour ai)
                {
                    var currentScore = ai.Evaluate();

                }
            }
        });
    }
}

/* 
### Техническая документация:

#### 1. Назначение:
Этот код определяет систему для управления ускорения персонажа в игре на основе пользовательского ввода.

#### 2. Ключевые особенности:
- Система работает с сущностями, которые имеют компоненты InputData, UserInputData.
- При каждом обновлении системы персонаж ускоряется в соответствии с данными ввода.
- Для выполнения конкретных действий перемещения используется компонент UserInputData, который содержит ссылку на экземпляр интерфейса moveAbility.

#### 3. Использование:
Эта система может быть применена к сущностям, которые должны ускорять перемещение по миру игры на основе пользовательского ввода.
*/
