using System.Collections.Generic;
using Assets.ECS_2.interfaces;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Tilemaps;
using DefaultNamespace;

public class CollisionAbility : MonoBehaviour, IConvertGameObjectToEntity, IAbility
{

    public Collider Collider;

    public List<MonoBehaviour> collisionActions = new List<MonoBehaviour>(); 
    public List<IAbilityTarget> collisionActionsAbiliities = new List<IAbilityTarget>();

    [HideInInspector]public List<Collider> collisions;

    /// <summary>
    /// ��������� ���� �� � ����� collisionsActions collisionAbility
    /// </summary>
    private void Start() 
    {
        foreach (var action in collisionActions)
        {
            if (action is IAbilityTarget ability) collisionActionsAbiliities.Add(ability); // �������� �������� �� action collisionAbility ��� ���

            else Debug.LogError("CollisionAction must derive from collisionAbility!");
        }
    }

    public void Execute()
    {
        foreach (var action in collisionActionsAbiliities) // �������� Execute() � �������� action � collisionActionsAbiliities
        {
            action.Targets = new List<GameObject>(); // ������������� Targets
            // ��������� � ������ Targets gameObject ������� �������� ������
            collisions.ForEach(c =>
            {
                if (c != null) action.Targets.Add(c.gameObject);
            });
            action.Execute();
        }
    }


    /// <summary>
    /// ������������� ��������� ���������� ��������� � ����������(�������)
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="dstManager"></param>
    /// <param name="conversionSystem"></param>
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        float3 position = gameObject.transform.position;
        switch (Collider)
        {
            case SphereCollider sphere:
                sphere.ToWorldSpaceSphere(out var sphereCenter, out var sphereRaduis);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = (Tile.ColliderType)ColliderType.Sphere,
                    SphereCenter = sphereCenter - position,
                    SphereRadius = sphereRaduis,
                    initialTakeOff = true
                });
                break;

            case CapsuleCollider capsule:
                capsule.ToWorldSpaceCapsule(out var capsuleStart, out var capsuleEnd, out var capsuleRadius);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = (Tile.ColliderType)ColliderType.Capsule,
                    CapsuleStart = capsuleStart - position,
                    CapsuleEnd = capsuleEnd - position,
                    CapsuleRadius = capsuleRadius,
                    initialTakeOff = true
                });
                break;

            case BoxCollider box:
                box.ToWorldSpaceBox(out var boxCenter, out var boxHalfExtents, out var boxOrientation);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = (Tile.ColliderType)ColliderType.Box,
                    BoxCenter = boxCenter - position,
                    BoxHalfExterns = boxHalfExtents,
                    BoxOrientation = boxOrientation,
                    initialTakeOff = true
                });
                break;
        }
    }
}

public struct ActorColliderData : IComponentData // ���� ������� ��������� ������� �������� 
{
    public Tile.ColliderType ColliderType;
    public float3 SphereCenter;
    public float3 CapsuleStart;
    public float3 CapsuleEnd;
    public float3 BoxCenter;
    public float3 BoxHalfExterns;
    public float SphereRadius;
    public float CapsuleRadius;
    public quaternion BoxOrientation;
    public bool initialTakeOff;
}

public enum ColliderType
{
    Sphere = 0, 
    Capsule = 1,
    Box = 2
}
