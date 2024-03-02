using DefaultNamespace;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// ����� ��������� �� MonoBehaviour � ��������� ��������� IConvertGameObjectToEntity, ��� ��������� ��� ���� ��������������� � �������� Unity.
/// </summary>
public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity 
{
     public float speed;
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
    public float boost;
    public float Speed;
}

/// <summary>
/// ������������ ������, ��������� �� ���������, ������� ����� ���� ��������� � ��������, ����� ����������, ��� ������ ����� ��������.
/// </summary>
public struct ShootData : IComponentData 
{
    public float shoot;
}

/* 
### ����������� ������������:

#### 1. ����������:
���� ��� ���������� ����� UserInputData, ������� ������������ ��� ���������� ������ � �������� ������ �������� �������.

#### 2. �������� �����������:
- ���������� speed � boost ���������� �������� �������������� �������.
- Convert ����� ������������ ��� �������������� ������� � �������� Unity � ���������� ��������������� ��������� ������.
- InputData, ShootData � InterfaceData ���������� ��������� ������ ��� �������� ������ ����������.

#### 3. �������������:
���� ����� ����� ���� �������� � ������� ��������, ����� ��������� �� ���������� �� ������ ����������������� ����� � ������ ����������.
*/