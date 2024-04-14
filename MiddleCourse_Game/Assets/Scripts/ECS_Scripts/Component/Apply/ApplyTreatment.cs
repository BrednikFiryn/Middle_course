using UnityEngine;

public class ApplyTreatment : MonoBehaviour, ICraftable
{
    [SerializeField] private float treatment;
    public string name = "";

    public string Name => name;

    public void Execute()
    {
            var character = FindObjectOfType<HealthBar>();
            if (character == null) return;
            character.Healing(treatment);
            character.HealthCheck();

            Destroy(this.gameObject);
    }
}
