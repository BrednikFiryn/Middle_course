using DefaultNamespace;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PickUpAbillity : MonoBehaviour, IAbilityTarget, IConvertGameObjectToEntity, IItem
{
     private Entity _entity;
     private EntityManager _dstManager;

    public List<GameObject> targets { get; set; }
    public GameObject uIItem;
    public GameObject UIItem => uIItem;

    public void Execute()
    {
        foreach (var target in targets)
        {
            var character = target.GetComponent<CharacterData>();
            if (character == null) return;
            if (character != null) character.Score(3);
            var item = Object.Instantiate(UIItem, character.InventoryUIRoot.transform, false);
            _dstManager.DestroyEntity(_entity);
            Destroy(this.gameObject);
        }
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        _entity = entity;
        _dstManager = dstManager;
    }
}
