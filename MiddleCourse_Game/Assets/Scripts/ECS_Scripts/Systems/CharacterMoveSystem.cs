using DefaultNamespace;
using Unity.Entities;
using UnityEngine;

/// <summary>
/// Класс наследует от ComponentSystem, что позволяет ему обрабатывать сущности Unity в системной манере.
/// </summary>
public class CharacterMoveSystem : ComponentSystem
{
    //Объявление переменной moveQuery для запроса сущностей.
    private EntityQuery moveQuery;

    /// <summary>
    /// Вызывается при создании системы и устанавливает запрос для получения сущностей.
    /// </summary>
    protected override void OnCreate()
    {      
        // Инициализация переменной moveQuery запросом сущностей, содержащих компоненты InputData, Transform и UserInputData.
        moveQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
            ComponentType.ReadOnly<Transform>(), ComponentType.ReadOnly<UserInputData>());
    }

    /// <summary>
    /// Вызывается каждый кадр для обновления данных в компонентах сущностей. Обрабатывает логику перемещения персонажа.
    /// </summary>
    protected override void OnUpdate()
    {
        //- Цикл Entities.With(moveQuery).ForEach перебирает все сущности, удовлетворяющие запросу.
        //-Внутри цикла данные из компонентов сущностей используются для определения направления движения и перемещения персонажа.
          Entities.With(moveQuery).ForEach(
          //Деструктуризация параметров для доступа к компонентам каждой сущности: entity, inputData, input, transform.
          (Entity entity, ref InputData inputData, UserInputData input, Transform transform) =>
          {
              //Проверка, существует ли действие движения и является ли оно экземпляром класса moveAbility.
              if (input.moveAction != null && input.moveAction is moveAbility ability)
              {
                  //Создание вектора направления на основе данных ввода о движении.
                  Vector3 direction = new Vector3(inputData.move.x, 0, inputData.move.y);

                  //Проверка, является ли длина квадрата вектора направления менее 0.1.
                  if (direction.sqrMagnitude < 0.1f) return;

                  //Создание ссылки playerTransform на компонент Transform сущности.
                  ref var playerTransform = ref transform;
                  //Создание ссылки speed на значение скорости из компонента InputData.
                  ref var speed = ref inputData.Speed;
                  //Обновление позиции игрового объекта с учетом направления и скорости движения.
                  playerTransform.position += direction * speed;

                  ////Установка поворота игрового объекта в направлении движения.
                  //playerTransform.rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);

                  var rot = transform.rotation;
                  var newRot = Quaternion.LookRotation(Vector3.Normalize(direction));
                  if (newRot == rot) return;
                  transform.rotation = Quaternion.Lerp(rot, newRot, Time.DeltaTime * 10);
              }
          });
    }
}