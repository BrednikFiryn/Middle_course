using Assets.ECS_2.interfaces;
using System;
using UnityEngine;

public class ShootAbility : MonoBehaviour, IAbility
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _shootDelay;
    [SerializeField] private float _bulletSpeed = 100f;
    private float _shootTime = float.MinValue;

    public void Execute()
    {
        if (Time.time < _shootTime + _shootDelay) return;

        _shootTime = Time.time;

        if (_bullet != null)
        {
            _bullet.SetActive(true);
            var _transform = this.transform;
            _bullet.transform.position = _transform.position;
            Rigidbody rb = _bullet.GetComponent<Rigidbody>();
            rb.velocity = _transform.forward * _bulletSpeed;
        }

        else Debug.Log("[SHOOT ABILITY] No bullet prefab link!");
    }
}