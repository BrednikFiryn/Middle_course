using DefaultNamespace;
using Unity.Entities;
using UnityEngine;

/// <summary>
/// ����� ��������� �� ComponentSystem, ��� ��������� ��� ������������ �������� Unity � ��������� ������.
/// </summary>
public class CharacterBoostSystem1 : ComponentSystem
{
    //���������� ���������� boostQuery ��� ������� ���������.
    private EntityQuery boostQuery;

    /// <summary>
    /// ���������� ��� �������� ������� � ������������� ������ ��� ��������� ���������.
    /// </summary>
    protected override void OnCreate()
    {
        // // ������������� ���������� boostQuery �������� ���������, ���������� ���������� InputData, � UserInputData.
        boostQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
        ComponentType.ReadOnly<UserInputData>());
    }

    /// <summary>
    /// ���������� ������ ���� ��� ���������� ������ � ����������� ���������. ������������ ������ ����������� ���������.
    /// </summary>
    protected override void OnUpdate()
    {
        //- ���� Entities.With(moveQuery).ForEach ���������� ��� ��������, ��������������� �������.
        //-������ ����� ������ �� ����������� ��������� ������������ ��� ��������� ���������.
        Entities.With(boostQuery).ForEach(
          //���������������� ���������� ��� ������� � ����������� ������ ��������: entity, inputData, input, transform.
          (Entity entity, UserInputData input, ref InputData inputData) =>
          {
              //��������, ���������� �� �������� �������� � �������� �� ��� ����������� ������ moveAbility.
              if (input.boostAction is moveAbility ability)
              {

                  //�������� ������� ����������� �� ������ ������ ����� � ��������.
                  Vector3 direction = new Vector3(inputData.move.x, 0, inputData.move.y);

                  //�������� ��� �������� ����� boost > 0, �������� ��������� �� ����� ����, �������� �������� �� ����� ����
                  if (inputData.boost > 0f && input.boostAction != null && direction.sqrMagnitude > 0.1f)
                  {
                      //�������� ����� Speed = ������ ����� speed / 5
                      inputData.Speed = input.speed / 5;
                  }           

                  //���� ������� �� ����������� ��
                  else 
                  {
                      //�������� ����� Speed = ������ ����� speed / 10
                      inputData.Speed = input.speed / 10;
                      return;
                  }        
              }   
          });
    }
}