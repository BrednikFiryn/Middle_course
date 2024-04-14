using DefaultNamespace;
using Unity.Entities;
using UnityEngine;

/// <summary>
/// ����� ��������� �� ComponentSystem, ��� ��������� ��� ������������ �������� Unity � ��������� ������.
/// </summary>
public class CharacterShootSystem : ComponentSystem
{
    //���������� ���������� shootQuery ��� ������� ���������.
    private EntityQuery shootQuery;

    /// <summary>
    /// ���������� ��� �������� ������� � ������������� ������ ��� ��������� ���������.
    /// </summary>
    protected override void OnCreate()
    {
        // ������������� ���������� moveQuery �������� ���������, ���������� ���������� InputData, UserInputData, ShootData.
        shootQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
        ComponentType.ReadOnly<ShootData>(), ComponentType.ReadOnly<UserInputData>());
    }

    /// <summary>
    /// ���������� ������ ���� ��� ���������� ������ � ����������� ���������. ������������ ������ ����������� ���������.
    /// </summary>
    protected override void OnUpdate()
    {
        //- ���� Entities.With(moveQuery).ForEach ���������� ��� ��������, ��������������� �������.
        //-������ ����� ������ �� ����������� ��������� ������������ ��� ����������� ����������� �������� � ����������� ���������.
        Entities.With(shootQuery).ForEach(
          //���������������� ���������� ��� ������� � ����������� ������ ��������: entity, inputData, input.
          (Entity entity, UserInputData input, ref ShootData shootData) =>
          {
              //��������, ��� �������� �������� ���������� � �������� �� ��� ����������� ������ IAbility.
              if (input.shootAction != null && input.shootAction is IAbility ability)
              {
                  //��������, ��� �������� ����� shoot > 0
                  if (shootData.shoot > 0f)
                  {
                      //����� ������ Execute() ��� ���������� �������� ��������.
                      ability.Execute();
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
- ������� �������� � ����������, ������� ����� ���������� InputData, UserInputData, ShootData.
- ��� ������ ���������� ������� �������� ���������� � ������������ � ������� �����.
- ��� ���������� ���������� �������� ����������� ������������ ��������� UserInputData, ������� �������� ������ �� ��������� ���������� IAbility.

#### 3. �������������:
��� ������� ����� ���� ��������� � ���������, ������� ������ ��������� �������� � ���� �� ������ ����������������� �����.
*/

