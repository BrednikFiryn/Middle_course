using Assets.ECS_2.interfaces;
using Unity.Entities;

public class CharacterShootSystem : ComponentSystem
{
    private EntityQuery shootQuery;

    protected override void OnCreate()
    {
        // ��������� ������ � Entity
        shootQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
        ComponentType.ReadOnly<ShootData>(), ComponentType.ReadOnly<UserInputData>());
    }

    protected override void OnUpdate()
    {
        Entities.With(shootQuery).ForEach(
          (Entity entity, UserInputData inputData, ref InputData input) =>
          {
              if (input.shoot > 0f && inputData.ShootAction != null && inputData.ShootAction is IAbility ability)
              { 
                  ability.Execute(); // ���� ���� ���������, ������ ����           
              }                     // ��������� iAbility - � ���� ���� ����� Execute() 
          });
    }
}

