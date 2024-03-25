using DefaultNamespace;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class CharacterAnimDeathSystem : ComponentSystem
{
    private EntityQuery moveQuery;

    protected override void OnCreate()
    {
        moveQuery = GetEntityQuery(ComponentType.ReadOnly<AnimData>(), ComponentType.ReadOnly<Animator>());
    }

    protected override void OnUpdate()
    {
        Entities.With(moveQuery).ForEach(
          (Entity entity, ref InputData move, UserInputData inputData, Animator animator) =>
          {
              Vector3 direction = new Vector3(move.move.x, 0, move.move.y);

              if (inputData.moveAction is moveAbility ability)
              {
                  if (move.boost == 0f && inputData.moveAction != null)
                  {
                      animator.SetBool(inputData.moveAnimHash, direction.sqrMagnitude > 0.1f);

                      if (inputData.moveAnimHash == string.Empty) return;

                      animator.SetFloat(inputData.moveSpeedAnimHash, inputData.speed + math.distancesq(move.move.x, move.move.y));
                      animator.SetBool(inputData.boostAnimHash, false); // Установка boostAnimHash в false при отсутствии ускорения.
                  }

                  else if (move.boost > 0f && inputData.boostAction != null)
                  {
                      animator.SetBool(inputData.boostAnimHash, direction.sqrMagnitude > 0.1f);
                      animator.SetBool(inputData.moveAnimHash, false); // Установка moveAnimHash в false при наличии ускорения.
                  }

                  else
                  {
                      animator.SetBool(inputData.moveAnimHash, false); // Установка moveAnimHash в false при отсутствии действий.
                      animator.SetBool(inputData.boostAnimHash, false); // Установка boostAnimHash в false при отсутствии действий.
                  }
              }
          });
    }
}