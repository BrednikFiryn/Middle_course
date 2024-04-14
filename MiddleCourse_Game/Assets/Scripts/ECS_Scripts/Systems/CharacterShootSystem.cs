using DefaultNamespace;
using Unity.Entities;
using UnityEngine;

/// <summary>
/// Класс наследует от ComponentSystem, что позволяет ему обрабатывать сущности Unity в системной манере.
/// </summary>
public class CharacterShootSystem : ComponentSystem
{
    //Объявление переменной shootQuery для запроса сущностей.
    private EntityQuery shootQuery;

    /// <summary>
    /// Вызывается при создании системы и устанавливает запрос для получения сущностей.
    /// </summary>
    protected override void OnCreate()
    {
        // Инициализация переменной moveQuery запросом сущностей, содержащих компоненты InputData, UserInputData, ShootData.
        shootQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
        ComponentType.ReadOnly<ShootData>(), ComponentType.ReadOnly<UserInputData>());
    }

    /// <summary>
    /// Вызывается каждый кадр для обновления данных в компонентах сущностей. Обрабатывает логику перемещения персонажа.
    /// </summary>
    protected override void OnUpdate()
    {
        //- Цикл Entities.With(moveQuery).ForEach перебирает все сущности, удовлетворяющие запросу.
        //-Внутри цикла данные из компонентов сущностей используются для определения направления движения и перемещения персонажа.
        Entities.With(shootQuery).ForEach(
          //Деструктуризация параметров для доступа к компонентам каждой сущности: entity, inputData, input.
          (Entity entity, UserInputData input, ref ShootData shootData) =>
          {
              //Проверка, что действия стрельбы существует и является ли оно экземпляром класса IAbility.
              if (input.shootAction != null && input.shootAction is IAbility ability)
              {
                  //Проверка, что значение ввода shoot > 0
                  if (shootData.shoot > 0f)
                  {
                      //Вызов метода Execute() для выполнения действия стрельбы.
                      ability.Execute();
                  }                     
              }         
          });
    }
}

/* 
### Техническая документация:

#### 1. Назначение:
Этот код определяет систему для управления стрельбой персонажа в игре на основе пользовательского ввода.

#### 2. Ключевые особенности:
- Система работает с сущностями, которые имеют компоненты InputData, UserInputData, ShootData.
- При каждом обновлении системы персонаж ускоряется в соответствии с данными ввода.
- Для выполнения конкретных действий перемещения используется компонент UserInputData, который содержит ссылку на экземпляр интерфейса IAbility.

#### 3. Использование:
Эта система может быть применена к сущностям, которые должны совершать стрельбу в игре на основе пользовательского ввода.
*/

