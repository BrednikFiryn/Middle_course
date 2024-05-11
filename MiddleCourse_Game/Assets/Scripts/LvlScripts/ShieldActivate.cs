using System.Collections;
using UnityEngine;

public class ShieldActivate : MonoBehaviour
{
   [SerializeField] private float _shieldDelay;
   [SerializeField] private CollisionShield _shield;
    private bool _shieldCondition = false;

    private void Start()
    {
        _shield = FindObjectOfType<CollisionShield>();
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
        yield return new WaitForSeconds(_shieldDelay);
        _shield.gameObject.SetActive(false);
        _shieldCondition = false;
    }

}
