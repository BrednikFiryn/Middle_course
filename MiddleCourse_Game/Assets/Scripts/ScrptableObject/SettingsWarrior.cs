using UnityEngine;

[CreateAssetMenu (fileName = "Player", menuName = "Player Settings")]
public class SettingsWarrior : ScriptableObject
{
    public float health;
    public float maxHealth;
    public float speed;
    public float damage;

    public GameObject hero;
}
