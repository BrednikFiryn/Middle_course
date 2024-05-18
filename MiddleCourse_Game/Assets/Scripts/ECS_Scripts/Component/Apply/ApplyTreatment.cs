using UnityEngine;

public class ApplyTreatment : MonoBehaviour
{
    [SerializeField] private float treatment;
    [SerializeField] private AK.Wwise.Event treatmentEvent = null;

    public void Execute()
    {
        var character = FindObjectOfType<HealthBar>();
        if (character == null) return;
        character.Healing(treatment);
        character.HealthCheck();
        treatmentEvent.Post(gameObject);

        Destroy(this.gameObject);
    }
}
