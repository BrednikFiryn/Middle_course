using Unity.Entities;

public class AIBehaveSystem : ComponentSystem
{
    private EntityQuery _evaluateQuery;

    //
    protected override void OnCreate()
    {
        _evaluateQuery = GetEntityQuery(ComponentType.ReadOnly<AIAgent>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_evaluateQuery).ForEach(
        (Entity entity, BehaviourManager manager) =>
        {
             manager.activeBehaviour?.Behave();
        });
    }
}