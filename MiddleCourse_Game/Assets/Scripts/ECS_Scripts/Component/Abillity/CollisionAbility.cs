using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Tilemaps;
using DefaultNamespace;

/// <summary>
/// ����� CollisionAbility �������� MonoBehaviour � ��������� ���������� IConvertGameObjectToEntity � IAbility.
/// �� ��������� ����������� �������� � ���������� ��������� � ���������� �������� � ����� Unity ECS (Entity Component System).
/// </summary>
public class CollisionAbility : MonoBehaviour, IConvertGameObjectToEntity, IAbility
{

    public Collider Collider;

    public List<MonoBehaviour> collisionActions = new List<MonoBehaviour>(); 
    public List<IAbilityTarget> collisionActionsAbiliities = new List<IAbilityTarget>();

    [HideInInspector] public List<Collider> collisions;

    /// <summary>
    /// ���������� ������ ������������ ��������.
    /// ���� �������� ��������� ��������� IAbilityTarget, ��������� ��� � ������ collisionActionsAbilities.
    /// � ��������� ������ ������������ ������.
    /// </summary>
    private void Start() 
    {
        foreach (var action in collisionActions)
        {
            if (action is IAbilityTarget ability) collisionActionsAbiliities.Add(ability); // �������� �������� �� action collisionAbility ��� ���

            else Debug.LogError("CollisionAction must derive from collisionAbility!");
        }
    }

    /// <summary>
    /// ���������� ������ �������� � collisionActionsAbilities.
    /// �������������� ���� � ��������� �������������� ������� ������� � ������ ����� ������� ��������.
    /// ��������� ��������.
    /// </summary>
    public void Execute()
    {
        foreach (var action in collisionActionsAbiliities) // �������� Execute() � �������� action � collisionActionsAbiliities
        {
            action.targets = new List<GameObject>(); // ������������� Targets
            // ��������� � ������ Targets gameObject ������� �������� ������
            collisions.ForEach(_collision =>
            {
                if (_collision != null) action.targets.Add(_collision.gameObject);
            });
            action.Execute();
        }
    }

    /// <summary>
    /// ����������� ��������� ���������� ���������� � ���������� (���) ����������.
    /// ���������� ��� ����������(�����, ������� ��� ��������������) � ��������� ��������������� ������ � �������� ���������.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="dstManager"></param>
    /// <param name="conversionSystem"></param>
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        //�������� ������� �������� ������� gameObject � ���������� ������������ � ��������� �� � ���������� position.
        float3 position = gameObject.transform.position;

        //�������� �������� ���� ����������, ��������� ����������� switch. Collider - ��� ����������, �������������� ���������, ������� �� ����� ���������������.
        switch (Collider)
        {
            //���� ��� ���������� ��������� � SphereCollider, ��������� ��������� ���� ����.
            case SphereCollider sphere:
                //�������� ����� ToWorldSpaceSphere, ����� ������������� ��������� ���������� ����� � ���������� ����������.
                //����������(����� ����� sphereCenter � ������ ����� sphereRadius) ����������� � ��������� ����������.
                sphere.ToWorldSpaceSphere(out var sphereCenter, out var sphereRaduis);
                //��������� ��������� ������ ActorColliderData � �������� ��������� dstManager ��� �������� entity
                //������� ����� ��������� ActorColliderData � ������� ��������� ����� new.
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    //������������� ��� ���������� � Sphere, ���������� �������� �� ������������ ColliderType � ��� Tile.ColliderType.
                    ColliderType = (Tile.ColliderType)ColliderType.Sphere,
                    //������������� ����� �����, ������� ������� ������� position �� ���������� ��������� ������ ����� sphereCenter.
                    SphereCenter = sphereCenter - position,
                    //������������� ������ �����.
                    SphereRadius = sphereRaduis,
                    //������������� ���� initialTakeOff � true.
                    initialTakeOff = true
                });
                break;

            //���� ��� ���������� ��������� � CapsuleCollider, ��������� ��������� ���� ����.
            case CapsuleCollider capsule:
                //�������� ����� ToWorldSpaceCapsule, ����� ������������� ��������� ���������� ������� � ���������� ����������.
                //����������(��� ������� capsuleStart, ���� ������� capsuleEnd � ������ ������� capsuleRadius) ����������� � ��������� ����������.
                capsule.ToWorldSpaceCapsule(out var capsuleStart, out var capsuleEnd, out var capsuleRadius);
                //��������� ��������� ������ ActorColliderData � �������� ��������� dstManager ��� �������� entity
                //������� ����� ��������� ActorColliderData � ������� ��������� ����� new.
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    //������������� ��� ���������� � Capsule, ���������� �������� �� ������������ ColliderType � ��� Tile.ColliderType.
                    ColliderType = (Tile.ColliderType)ColliderType.Capsule,
                    //������������� ��� �������, ������� ������� ������� position �� ���������� ��������� ���� ������� capsuleStart.
                    CapsuleStart = capsuleStart - position,
                    //������������� ���� �������, ������� ������� ������� position �� ���������� ��������� ���� ������� CapsuleEnd.
                    CapsuleEnd = capsuleEnd - position,
                    //������������� ������ �������.
                    CapsuleRadius = capsuleRadius,
                    //������������� ���� initialTakeOff � true.
                    initialTakeOff = true
                });
                break;

            // //���� ��� ���������� ��������� � BoxCollider, ��������� ��������� ���� ����.
            case BoxCollider box:
                //�������� ����� ToWorldSpaceBox, ����� ������������� ��������� ���������� ���� � ���������� ����������.
                //����������(����� ���� boxCenter, ���������� ���� boxHalfExtents � ���������� ���� boxOrientation) ����������� � ��������� ����������.
                box.ToWorldSpaceBox(out var boxCenter, out var boxHalfExtents, out var boxOrientation);
                //��������� ��������� ������ ActorColliderData � �������� ��������� dstManager ��� �������� entity
                //������� ����� ��������� ActorColliderData � ������� ��������� ����� new.
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    //������������� ��� ���������� � Box, ���������� �������� �� ������������ ColliderType � ��� Tile.ColliderType.
                    ColliderType = (Tile.ColliderType)ColliderType.Box,
                    //������������� ����� ����, ������� ������� ������� position �� ���������� ��������� ���� ������� boxCenter.
                    BoxCenter = boxCenter - position,
                    //������� ���������� ����.
                    BoxHalfExterns = boxHalfExtents,
                    //������� ���������� ����.
                    BoxOrientation = boxOrientation,
                    //������������� ���� initialTakeOff � true.
                    initialTakeOff = true
                });
                break;
        }

        Collider.enabled = false;
    }
}

/* 
���� ����� ��������� ����������� ��������� ����� ����������� (�����, �������, ��������������) 
� ��������������� ������ �������� ActorColliderData, ������� ��� ������������� � Unity ECS.
*/

/// <summary>
/// ������������ ������, ����������� ������� ���������.
/// �������� ���� ��� ���� ����������, ������, ���������/�������� ����� (��� �������), 
/// �������� ������(��� ���������������), �������, ����������(��� ���������������) � ����� ���������� ������.
/// </summary>
public struct ActorColliderData : IComponentData
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

/// <summary>
/// ����������� ��������� ���� ����������� (�����, ������� � ��������������).
/// </summary>
public enum ColliderType
{
    Sphere = 0, 
    Capsule = 1,
    Box = 2
}

/* 
### ����������� ������������:

#### ����������:
- ���� ������ ������������ ��� ���������� � Unity ECS � ������������ ������� ���������� ���������������� Unity ECS � �����������.
- �� ��������� �������������� ������� �������� � ������������ � �������� ECS.
- ������������� �������� ���������� ��������, ��������� � ����������, � ����� ECS.
- ��� ���������� ������������� � ������������� ��������� ���������� � �������� ���������� � ��������� Unity ECS.
*/
