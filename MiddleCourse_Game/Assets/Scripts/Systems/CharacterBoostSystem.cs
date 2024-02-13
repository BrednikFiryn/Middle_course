using Assets.ECS_2.interfaces;
using Unity.Entities;
using UnityEngine;

/// <summary>
/// ����� ��������� �� ComponentSystem, ��� ��������� ��� ������������ �������� Unity � ��������� ������.
/// </summary>
public class CharacterBoostSystem1 : ComponentSystem
{
    //���������� ���������� moveQuery ��� ������� ���������.
    private EntityQuery boostQuery;

    /// <summary>
    /// ���������� ��� �������� ������� � ������������� ������ ��� ��������� ���������.
    /// </summary>
    protected override void OnCreate()
    {
        // // ������������� ���������� moveQuery �������� ���������, ���������� ���������� InputData, Transform � UserInputData.
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
                      ability.Execute(); // ���� ���� ���������, ������ ����           
                  }                     // ��������� BstAbility - � ���� ���� ����� Execute() 
                
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
