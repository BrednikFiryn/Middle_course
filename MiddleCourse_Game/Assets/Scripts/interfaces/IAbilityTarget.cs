using UnityEngine;
using System.Collections.Generic;
namespace Assets.ECS_2.interfaces

{

    public interface IAbilityTarget : IAbility
    {
        public List<GameObject> Targets { get; set; }
    }

}
