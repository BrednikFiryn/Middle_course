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
              if (input.BoostAction is moveAbility ability)
              {

                  //�������� ������� ����������� �� ������ ������ ����� � ��������.
                  Vector3 direction = new Vector3(inputData.move.x, 0, inputData.move.y);

                  //�������� ��� �������� ����� boost > 0, �������� ��������� �� ����� ����, �������� �������� �� ����� ����
                  if (inputData.boost > 0f && input.BoostAction != null)
                  {

                      //��������, �������� �� ����� �������� ������� ����������� ����� 0.1.
                      if (direction.sqrMagnitude < 0.1f)
                      {
                          //����� ������ Stop() ��� ��������� ���������� �������� ��������.
                          ability.Stop();
                          return;
                      }

                      //�������� ����� Speed = ������ ����� speed / 5
                      inputData.Speed = input.speed / 5;
                      //����� ������ Execute() ��� ���������� �������� ���������.
                      ability.Execute(); 
                  }           

                  //���� ������� �� ����������� ��
                  else 
                  {
                      //�������� ����� Speed = ������ ����� speed / 10
                      inputData.Speed = input.speed / 10;
                      //����� ������ Stop() ��� ��������� ���������� �������� ���������.
                      ability.Stop();
                      return;
                  }        
              }   
          });
    }
}

/* 
### ����������� ������������:

#### 1. ����������:
���� ��� ���������� ������� ��� ���������� ��������� ��������� � ���� �� ������ ����������������� �����.

#### 2. �������� �����������:
- ������� �������� � ����������, ������� ����� ���������� InputData, UserInputData.
- ��� ������ ���������� ������� �������� ���������� � ������������ � ������� �����.
- ��� ���������� ���������� �������� ����������� ������������ ��������� UserInputData, ������� �������� ������ �� ��������� ���������� moveAbility.

#### 3. �������������:
��� ������� ����� ���� ��������� � ���������, ������� ������ �������� ����������� �� ���� ���� �� ������ ����������������� �����.
*/
