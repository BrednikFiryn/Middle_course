using DefaultNamespace;
using Unity.Entities;
using UnityEngine;

/// <summary>
/// Класс наследует от ComponentSystem, что позволяет ему обрабатывать сущности Unity в системной манере.
/// </summary>
public class CharacterBoostSystem1 : ComponentSystem
{
    //Объявление переменной boostQuery для запроса сущностей.
    private EntityQuery boostQuery;

    /// <summary>
    /// Вызывается при создании системы и устанавливает запрос для получения сущностей.
    /// </summary>
    protected override void OnCreate()
    {
        // // Инициализация переменной boostQuery запросом сущностей, содержащих компоненты InputData, и UserInputData.
        boostQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
        ComponentType.ReadOnly<UserInputData>());
    }

    /// <summary>
    /// Вызывается каждый кадр для обновления данных в компонентах сущностей. Обрабатывает логику перемещения персонажа.
    /// </summary>
    protected override void OnUpdate()
    {
        //- Цикл Entities.With(moveQuery).ForEach перебирает все сущности, удовлетворяющие запросу.
        //-Внутри цикла данные из компонентов сущностей используются для ускорения персонажа.
        Entities.With(boostQuery).ForEach(
          //Деструктуризация параметров для доступа к компонентам каждой сущности: entity, inputData, input, transform.
          (Entity entity, UserInputData input, ref InputData inputData) =>
          {
              //Проверка, существует ли действие движения и является ли оно экземпляром класса moveAbility.
              if (input.BoostAction is moveAbility ability)
              {

                  //Создание вектора направления на основе данных ввода о движении.
                  Vector3 direction = new Vector3(inputData.move.x, 0, inputData.move.y);

                  //Проверка что значение ввода boost > 0, действие ускорения не равно нулю, действия движения не равно нулю
                  if (inputData.boost > 0f && input.BoostAction != null)
                  {

                      //Проверка, является ли длина квадрата вектора направления менее 0.1.
                      if (direction.sqrMagnitude < 0.1f)
                      {
                          //Вызов метода Stop() для остановки выполнения действия движения.
                          ability.Stop();
                          return;
                      }

                      //значение ввода Speed = данные ввода speed / 5
                      inputData.Speed = input.speed / 5;
                      //Вызов метода Execute() для выполнения действия ускорения.
                      ability.Execute(); 
                  }           

                  //Если условие не выполняется то
                  else 
                  {
                      //значение ввода Speed = данные ввода speed / 10
                      inputData.Speed = input.speed / 10;
                      //Вызов метода Stop() для остановки выполнения действия ускорения.
                      ability.Stop();
                      return;
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
