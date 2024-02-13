using Assets.ECS_2.interfaces;
using Unity.Entities;
using UnityEngine;

/// <summary>
/// Класс наследует от ComponentSystem, что позволяет ему обрабатывать сущности Unity в системной манере.
/// </summary>
public class CharacterBoostSystem1 : ComponentSystem
{
    //Объявление переменной moveQuery для запроса сущностей.
    private EntityQuery boostQuery;

    /// <summary>
    /// Вызывается при создании системы и устанавливает запрос для получения сущностей.
    /// </summary>
    protected override void OnCreate()
    {
        // // Инициализация переменной moveQuery запросом сущностей, содержащих компоненты InputData, Transform и UserInputData.
        boostQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
        ComponentType.ReadOnly<UserInputData>());
    }

    protected override void OnUpdate()
    {
        Entities.With(boostQuery).ForEach(
          (Entity entity, UserInputData input, ref InputData inputData) =>
          {
              if (input.BoostAction is moveAbility ability)
              {
                  if (inputData.boost > 0f && input.BoostAction != null)
                  {
                      inputData.Speed = input.speed / 5;
                      ability.Execute(); // если поле заполнено, значит поле           
                  }                     // наследник BstAbility - у поля есть метод Execute() 
                
                  else 
                  {
                      inputData.Speed = input.speed / 10;
                      ability.Stop();
                      return;
                  }        
              }   
              
          });
    }
}
