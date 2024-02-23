using Assets.ECS_2.interfaces;
using Unity.Entities;

public class AIEvaluateSystem : ComponentSystem
{
    private EntityQuery _evaluateQuery;

    /// <summary>
    /// �������������� ����� OnCreate �� ComponentSystem.
    /// </summary>
    protected override void OnCreate()
    {
        // �������������� _evaluateQuery ��� ������� ��������� � ����������� AIAgent.
        _evaluateQuery = GetEntityQuery(ComponentType.ReadOnly<AIAgent>());
    }

    /// <summary>
    /// �������������� ����� OnUpdate �� ComponentSystem.
    /// </summary>
    protected override void OnUpdate()
    {
        // ���������� �������� � ������������ ������������.
        Entities.With(_evaluateQuery).ForEach(
        // ������-�������, ������������ �������� ��� ������ �������� � �� ���������������� BehaviourManager.
        (Entity entity, BehaviourManager manager) =>
        {
            // �������������� hightScore ����� ������ ��������� ���������.
            float hightScore = float.MinValue;
            // ���������� �������� ��������� ���������.
            manager.activeBehaviour = null;
            //  ���������� ��������� � ���������.
            foreach (var behaviour in manager._behaviours)
            {
                // ���������, ��������� �� ��������� ��������� IBehaviour.
                if (behaviour is IBehaviour ai)
                {
                    // ��������� ������� ��������� � ��������� ����.
                    var currentScore = ai.Evaluate();
                    // ��������� ��������� ���� � �������� ���������, ���� ������� ���� ����.
                    if (currentScore > hightScore)
                    {
                        hightScore = currentScore;
                        manager.activeBehaviour = ai;
                    }
                }
            }
        });
    }
}