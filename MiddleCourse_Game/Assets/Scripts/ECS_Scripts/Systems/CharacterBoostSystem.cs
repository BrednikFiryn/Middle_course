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
              if (input.boostAction is moveAbility ability)
              {

                  //Создание вектора направления на основе данных ввода о движении.
                  Vector3 direction = new Vector3(inputData.move.x, 0, inputData.move.y);

                  //Проверка что значение ввода boost > 0, действие ускорения не равно нулю, действия движения не равно нулю
                  if (inputData.boost > 0f && input.boostAction != null && direction.sqrMagnitude > 0.1f)
                  {
                      //значение ввода Speed = данные ввода speed / 5
                      inputData.Speed = input.speed / 5;
                  }           

                  //Если условие не выполняется то
                  else 
                  {
                      //значение ввода Speed = данные ввода speed / 10
                      inputData.Speed = input.speed / 10;
                      return;
                  }        
              }   
          });
    }
}