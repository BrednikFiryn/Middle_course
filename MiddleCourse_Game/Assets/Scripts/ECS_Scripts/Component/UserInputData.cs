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
     public string moveAnimHash;
     public string moveSpeedAnimHash;
     public string boostAnimHash;
     public MonoBehaviour shootAction;
     public MonoBehaviour rotationAction;
     public MonoBehaviour moveAction;
     public MonoBehaviour boostAction;

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
        // AddComponentData: ��������� ���������� ������ � ��������, ����� ��� InputData, ShootData.
        dstManager.AddComponentData(entity, new InputData());

        // ���������, ��������� �� ���������� ShootAction � �������� �� ��� ����������� ���������� IAbility,
        // �, ���� ��� ���, ��������� ��������� ShootData.
        if (shootAction != null && shootAction is IAbility)
        {
            dstManager.AddComponentData(entity, new ShootData());
        }

        if (moveAnimHash != string.Empty)
        {
            dstManager.AddComponentData(entity, new AnimData());
        }
    }
}

/// <summary>
/// ���������� ������ �����, ����� ��� ������ ��������, �������� ��������, �������� ��������� � ��������.
/// </summary>
public struct InputData : IComponentData
{
    public float2 move;
    public float2 rotation;
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

/// <summary>
/// ������������ ������, ��������� � ���������.
/// </summary>
public struct AnimData : IComponentData
{

}