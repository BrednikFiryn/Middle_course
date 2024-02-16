using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    private UserInputData health;

    private void Start()
    {
        health = GetComponent<UserInputData>();
    }

    void Update()
    {
        HealthCheck();
    }

   private void HealthCheck()
    {
       healthBar.fillAmount = health.health;  
    }
}
