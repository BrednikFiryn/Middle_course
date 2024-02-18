using Assets.ECS_2.interfaces;
using System;
using UnityEngine;

public class ShootAbility : MonoBehaviour, IAbility
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _shootDelay;
    [SerializeField] public float _bulletSpeed = 100f;
    private float _shootTime = float.MinValue;

    public PlayerStats playerStats;

    private void PlayerStatsJson()
    {
        var jsonString = PlayerPrefs.GetString("Stats");
        if (!jsonString.Equals(String.Empty, StringComparison.Ordinal))
        {
            playerStats = JsonUtility.FromJson<PlayerStats>(jsonString);
        }

        else
        {
            playerStats = new PlayerStats();
        }
    }

    private void Start()
    {
        PlayerStatsJson();
    }

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
            playerStats.shootsCount++;
        }

        else Debug.Log("[SHOOT ABILITY] No bullet prefab link!");
    }
}