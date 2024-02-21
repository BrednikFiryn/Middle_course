using Assets.ECS_2.interfaces;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

/// <summary>
/// ����� ��������� �� ComponentSystem, ��� ��������� ��� ������������ �������� Unity � ��������� ������.
/// </summary>
public class AIEvaluateSystem : ComponentSystem
{
    //���������� ���������� moveQuery ��� ������� ���������.
    private EntityQuery _evaluateQuery;

    /// <summary>
    /// ���������� ��� �������� ������� � ������������� ������ ��� ��������� ���������.
    /// </summary>
    protected override void OnCreate()
    {
        // ������������� ���������� moveQuery �������� ���������, ���������� ��������� AIAgent;
        _evaluateQuery = GetEntityQuery(ComponentType.ReadOnly<AIAgent>());
    }

    /// <summary>
    /// ���������� ������ ���� ��� ���������� ������ � ����������� ���������. ������������ ������ ����������� ���������.
    /// </summary>
    protected override void OnUpdate()
    {
        //- ���� Entities.With(_evaluateQuery).ForEach ���������� ��� ��������, ��������������� �������.
        //-������ ����� ������ �� ����������� ��������� ������������ ��� ����������� ����������� �������� � ����������� ���������.
        Entities.With(_evaluateQuery).ForEach(
        //���������������� ���������� ��� ������� � ����������� �������� manager.
        (Entity entity, BehaviourManager manager) =>
        {
            IBehaviour bestBehaviour;
            float hightScore = float.MinValue;

            foreach (var behaviour in manager._behaviours)
            {
                if (behaviour is IBehaviour ai)
                {
                    var currentScore = ai.Evaluate();

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
