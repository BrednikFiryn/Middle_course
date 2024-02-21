//using Unity.Entities;
//using Unity.Transforms;
//using UnityEngine;

///// <summary>
///// Объявляется класс DestroySystem, который наследуется от ComponentSystem. Это означает, что класс будет использоваться для создания системы обновления компонентов.
///// </summary>
//public class DestroySystem : ComponentSystem
//{
//    /// <summary>
//    /// Этот метод переопределяется из базового класса ComponentSystem и вызывается каждый кадр для обновления системы.
//    /// </summary>
//    protected override void OnUpdate()
//    {
//        //Entities.WithAll<BulletBehaviour>(): Это вызов метода WithAll, который фильтрует только те сущности, которые имеют компонент BulletBehaviour.
//        //ForEach((Entity entity, ref Translation translation) => {: Запускается цикл перебора всех выбранных сущностей.
//        //В аргументах указывается тип сущности (Entity) и ссылка на компонент Translation, чтобы иметь доступ к его данным.
//        Entities.WithAll<BulletBehaviour>().ForEach((Entity entity, ref Translation translation) =>
//        {
//            // Получаем gameObject связанный с этой сущностью
//            GameObject gameObject = EntityManager.GetComponentObject<GameObject>(entity);
//            // Проверяет сущетвует ли объект если нет, уничтожает сущность.
//            if (gameObject == null)
//            {
//                Debug.Log("gameObject == null");
//                // Этот метод уничтожает сущность. Он использует PostUpdateCommands для выполнения команд после обновления системы, чтобы избежать конфликтов.
//                PostUpdateCommands.DestroyEntity(entity);
//            }
//        });
//    }
//}