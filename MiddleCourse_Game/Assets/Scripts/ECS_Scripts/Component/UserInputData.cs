using Assets.ECS_2.interfaces;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// ����� ��������� �� MonoBehaviour � ��������� ��������� IConvertGameObjectToEntity, ��� ��������� ��� ���� ��������������� � �������� Unity.
/// </summary>
public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity 
{
     public float speed;
     public float health;

     public MonoBehaviour ShootAction;
     public MonoBehaviour MoveAction;
     public MonoBehaviour BoostAction;

    /// <summary>
    /// �����, ������� ���������� ��� �������������� ������� � ��������. �� ��������� ���������� ������ � �������� �� ������ ���������� ������� � ��� ��������.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="dstManager"></param>
    /// <param name="conversionSystem"></param>
    /// Convert: ��������� ��������, �������� ��������� � ������� �������������� �������� �������.
    public void Convert(Entity entity, EntityManager dstManager,
        GameObjectConversionSystem conversionSystem)
    {
        // AddComponentData: ��������� ���������� ������ � ��������, ����� ��� InputData, ShootData � InterfaceData.
        dstManager.AddComponentData(entity, new InputData());
        dstManager.AddComponentData(entity, new InterfaceData { Health = health });

        // ���������, ��������� �� ���������� ShootAction � �������� �� ��� ����������� ���������� IAbility,
        // �, ���� ��� ���, ��������� ��������� ShootData.
        if (ShootAction != null && ShootAction is IAbility)
        {
            dstManager.AddComponentData(entity, new ShootData());
        }
    }
}

/// <summary>
/// ���������� ������ �����, ����� ��� ������ ��������, �������� ��������, �������� ��������� � ��������.
/// </summary>
public struct InputData : IComponentData
{
    public float2 move;
    public float shoot;
    public float boost;
    public float Speed;
}

/// <summary>
/// ������������ ������, ��������� �� ���������, ������� ����� ���� ��������� � ��������, ����� ����������, ��� ������ ����� ��������.
/// </summary>
public struct ShootData : IComponentData 
{

}

/// <summary>
/// �������� ���������� � �������� �������.
/// </summary>
public struct InterfaceData : IComponentData
{
    public float Health;
}

/* 
### ����������� ������������:

#### 1. ����������:
���� ��� ���������� ����� UserInputData, ������� ������������ ��� ���������� ������ � �������� ������ �������� �������.

#### 2. �������� �����������:
- ���������� speed, health � boost ���������� �������� �������������� �������.
- Convert ����� ������������ ��� �������������� ������� � �������� Unity � ���������� ��������������� ��������� ������.
- InputData, ShootData � InterfaceData ���������� ��������� ������ ��� �������� ������ ����������.

#### 3. �������������:
���� ����� ����� ���� �������� � ������� ��������, ����� ��������� �� ���������� �� ������ ����������������� ����� � ������ ����������.
*/