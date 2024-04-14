using Assets.ECS_2.interfaces;
using UnityEngine;

public class DamageReduction : MonoBehaviour, ILevelUp
{
    public void levelUp(CharacterData data, int level)
    {
        IBehaviour.damage -= IBehaviour.damage / 10;
    }
}
