using System.Collections.Generic;
using Assets.ECS_2.interfaces;
using Unity.Entities;
using UnityEngine;

public class BehaviourManager : MonoBehaviour, IConvertGameObjectToEntity
{

    public List<MonoBehaviour> _behaviours;
    public IBehaviour activeBehaviour;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponent<AIAgent>(entity);
    }
}

public struct AIAgent : IComponentData
{ }

