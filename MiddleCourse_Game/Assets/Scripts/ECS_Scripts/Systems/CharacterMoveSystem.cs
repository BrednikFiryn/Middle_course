using DefaultNamespace;
using Unity.Entities;
using UnityEngine;

/// <summary>
/// ����� ��������� �� ComponentSystem, ��� ��������� ��� ������������ �������� Unity � ��������� ������.
/// </summary>
public class CharacterMoveSystem : ComponentSystem
{
    //���������� ���������� moveQuery ��� ������� ���������.
    private EntityQuery moveQuery;

    /// <summary>
    /// ���������� ��� �������� ������� � ������������� ������ ��� ��������� ���������.
    /// </summary>
    protected override void OnCreate()
    {      
        // ������������� ���������� moveQuery �������� ���������, ���������� ���������� InputData, Transform � UserInputData.
        moveQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
            ComponentType.ReadOnly<Transform>(), ComponentType.ReadOnly<UserInputData>());
    }

    /// <summary>
    /// ���������� ������ ���� ��� ���������� ������ � ����������� ���������. ������������ ������ ����������� ���������.
    /// </summary>
    protected override void OnUpdate()
    {
        //- ���� Entities.With(moveQuery).ForEach ���������� ��� ��������, ��������������� �������.
        //-������ ����� ������ �� ����������� ��������� ������������ ��� ����������� ����������� �������� � ����������� ���������.
          Entities.With(moveQuery).ForEach(
          //���������������� ���������� ��� ������� � ����������� ������ ��������: entity, inputData, input, transform.
          (Entity entity, ref InputData inputData, UserInputData input, Transform transform) =>
          {
              //��������, ���������� �� �������� �������� � �������� �� ��� ����������� ������ moveAbility.
              if (input.moveAction != null && input.moveAction is moveAbility ability)
              {
                  //�������� ������� ����������� �� ������ ������ ����� � ��������.
                  Vector3 direction = new Vector3(inputData.move.x, 0, inputData.move.y);

                  //��������, �������� �� ����� �������� ������� ����������� ����� 0.1.
                  if (direction.sqrMagnitude < 0.1f) return;

                  //�������� ������ playerTransform �� ��������� Transform ��������.
                  ref var playerTransform = ref transform;
                  //�������� ������ speed �� �������� �������� �� ���������� InputData.
                  ref var speed = ref inputData.Speed;
                  //���������� ������� �������� ������� � ������ ����������� � �������� ��������.
                  playerTransform.position += direction * speed;

                  ////��������� �������� �������� ������� � ����������� ��������.
                  //playerTransform.rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);

                  var rot = transform.rotation;
                  var newRot = Quaternion.LookRotation(Vector3.Normalize(direction));
                  if (newRot == rot) return;
                  transform.rotation = Quaternion.Lerp(rot, newRot, Time.DeltaTime * 10);
              }
          });
    }
}