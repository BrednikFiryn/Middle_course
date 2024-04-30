using DefaultNamespace;
using UnityEngine;
using Zenject;

public class ShootAbility : MonoBehaviour, IAbility
{
    [SerializeField] private GameObject[] _bullet;
    [SerializeField] private int index = 0;
    [SerializeField] private float _shootDelay;
    [SerializeField] private float _overheating;
    [SerializeField] private float _bulletSpeed = 100f;
    [SerializeField] private AK.Wwise.Event shootEvent = null;
    private float _shootTime = float.MinValue;
    private float _shootDelayConst;

    private void Start()
    {
        _shootDelayConst = _shootDelay;
    }

    [Inject]
    public void Construct(BindBullet bindBullet)
    {
        _bullet = bindBullet.bullets;
    }

    public void Execute()
    {
        if (Time.time < _shootTime + _shootDelay) return;
        _shootDelay = _shootDelayConst;
        _shootTime = Time.time;

        if (_bullet != null)
        {
            Shooting();
            shootEvent.Post(this.gameObject);

            if (index < _bullet.Length) ++index;
            if (index == _bullet.Length)
            {
                index = 0;
                _shootDelay = _overheating;
            }
        }

        else Debug.Log("[SHOOT ABILITY] No bullet prefab link!");
    }

    private void Shooting()
    {
        _bullet[index].SetActive(true);
        var _transform = this.transform;
        _bullet[index].transform.position = _transform.position;
        Rigidbody rb = _bullet[index].GetComponent<Rigidbody>();
        rb.velocity = _transform.forward * _bulletSpeed;
    }
}