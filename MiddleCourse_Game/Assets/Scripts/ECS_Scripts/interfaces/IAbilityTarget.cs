using UnityEngine;
using System.Collections.Generic;

namespace DefaultNamespace
{
    public interface IAbilityTarget : IAbility
    {
        public List<GameObject> targets { get; set; }
    }
}
