using Assets.ECS_2.interfaces;
using Unity.Entities;

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
          (Entity entity, UserInputData input, ref InputData inputData) =>
          {
              //��������, ��� �������� ����� shoot > 0, �������� �������� ���������� � �������� �� ��� ����������� ������ moveAbility.
              if (inputData.shoot > 0f && input.ShootAction != null && input.ShootAction is IAbility ability)
              {
                  //����� ������ Execute() ��� ���������� �������� ��������.
                  ability.Execute();       
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

