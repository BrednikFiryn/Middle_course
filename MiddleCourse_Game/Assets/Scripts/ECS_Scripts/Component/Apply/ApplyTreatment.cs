using UnityEngine;

public class ApplyTreatment : MonoBehaviour
{
    [SerializeField] private float treatment;

    public void Execute()
    {
            var character = FindObjectOfType<HealthBar>();
            if (character == null) return;
            character.Healing(treatment);
            character.HealthCheck();

            Destroy(this.gameObject);
    }
}
