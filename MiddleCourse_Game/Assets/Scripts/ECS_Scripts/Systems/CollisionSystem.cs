using System;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Tilemaps;

namespace DefaultNamespace.Systems
{
    public class CollisionSystem : ComponentSystem
    {
        private EntityQuery collisionQuery;

        private Collider[] results = new Collider[50]; // то с чем мы сталкиваемся 

        protected override void OnCreate()
        {
            collisionQuery = GetEntityQuery(ComponentType.ReadOnly<ActorColliderData>(),
                ComponentType.ReadOnly<Transform>()); // заполняем collisionQuery
        }

        protected override void OnUpdate()
        {
            var dstManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            Entities.With(collisionQuery).ForEach((Entity entity, CollisionAbility abilityCollision, 
                ref ActorColliderData colliderData) => 
                {
                var gameObject = abilityCollision.gameObject; // кэшируем gameObject 
                float3 position = gameObject.transform.position; // кэшируем position
                Quaternion rotation = gameObject.transform.rotation; // кэшируем rotation


                    abilityCollision.collisions?.Clear(); // очищает список в каждом кадре

                int size = 0;

                switch (colliderData.ColliderType)

                {
                        // NonAllox - спец. метод в Unity не вызывает доп. аллокаций (мы просто проверяем столкновения)
                    case (Tile.ColliderType)ColliderType.Sphere:
                        size = Physics.OverlapSphereNonAlloc(colliderData.SphereCenter = position,
                            colliderData.SphereRadius, results);
                    break;

                    case (Tile.ColliderType)ColliderType.Capsule:
                        var center = ((colliderData.CapsuleStart + position) + (colliderData.CapsuleEnd + position)) / 2f;
                        var point1 = colliderData.CapsuleStart + position; 
                         var point2 = colliderData.CapsuleEnd + position;
                        point1 = (float3)(rotation * (point1 - center)) + center;
                        point2 = (float3)(rotation * (point2 - center)) + center;
                        size = Physics.OverlapCapsuleNonAlloc(point1, point2, colliderData.CapsuleRadius, results);
                        break;

                    case (Tile.ColliderType)ColliderType.Box:
                        size = Physics.OverlapBoxNonAlloc(colliderData.BoxCenter + position,
                            colliderData.BoxHalfExterns, results, colliderData.BoxOrientation * rotation);
                    break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                   if (size > 0)
                {      
                        foreach (var result in results)
                        {
                            abilityCollision?.collisions?.Add(result); // добавляет колизии в список при столкновении
                        }
                        abilityCollision.Execute();                   
                }
                });
        }
    }
}
