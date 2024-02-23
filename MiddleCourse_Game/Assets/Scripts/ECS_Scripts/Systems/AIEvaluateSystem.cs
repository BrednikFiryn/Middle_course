using Assets.ECS_2.interfaces;
using Unity.Entities;

public class AIEvaluateSystem : ComponentSystem
{
    private EntityQuery _evaluateQuery;

    /// <summary>
    /// Переопределяет метод OnCreate из ComponentSystem.
    /// </summary>
    protected override void OnCreate()
    {
        // Инициализирует _evaluateQuery для запроса сущностей с компонентом AIAgent.
        _evaluateQuery = GetEntityQuery(ComponentType.ReadOnly<AIAgent>());
    }

    /// <summary>
    /// Переопределяет метод OnUpdate из ComponentSystem.
    /// </summary>
    protected override void OnUpdate()
    {
        // Перебирает сущности с необходимыми компонентами.
        Entities.With(_evaluateQuery).ForEach(
        // Лямбда-функция, определяющая действия для каждой сущности и ее ассоциированного BehaviourManager.
        (Entity entity, BehaviourManager manager) =>
        {
            // Инициализирует hightScore самым низким возможным значением.
            float hightScore = float.MinValue;
            // Сбрасывает активное поведение менеджера.
            manager.activeBehaviour = null;
            //  Перебирает поведения в менеджере.
            foreach (var behaviour in manager._behaviours)
            {
                // Проверяет, реализует ли поведение интерфейс IBehaviour.
                if (behaviour is IBehaviour ai)
                {
                    // Оценивает текущее поведение и сохраняет балл.
                    var currentScore = ai.Evaluate();
                    // Обновляет наивысший балл и активное поведение, если текущий балл выше.
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