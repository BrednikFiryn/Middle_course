using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShieldActivate : MonoBehaviour
{
    [SerializeField] private float _shieldDelay;
    [SerializeField] private Image _shieldIamge;
    private CollisionShield _shield;
    private bool _shieldCondition = false;

    private void Start()
    {
        _shield = FindObjectOfType<CollisionShield>();
        _shield.gameObject.SetActive(false);
    }

    public void ButtonShieldActivate()
    {
        if (_shieldCondition == false)
        {
            _shield.gameObject.SetActive(true);
            _shieldCondition = true;
            StartCoroutine(ShieldCoroutine());
        }
    }

    private IEnumerator ShieldCoroutine()
    {
        _shieldIamge.fillAmount -= 1.0f / _shieldDelay * Time.deltaTime;
        yield return new WaitForSeconds(_shieldDelay);
        _shield.gameObject.SetActive(false);
        _shieldCondition = false;
        _shieldIamge.fillAmount = 1;
    }

}
