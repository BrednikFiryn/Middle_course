using System;
using UnityEngine;

public class CameraWork : MonoBehaviour
{
    #region Private
    [Tooltip("Рассояние в локальной плоскости x-z до цели")]
    [SerializeField] private float distance = 0.7f;
    [Tooltip("Высота, на которой мы хотим, чтобы камера находилась над целью")]
    [SerializeField] private float height = 3.0f;

    [Tooltip("Смещение камеры по вертикали от цели, чтобы лучше видеть сцену и уменьшить расстояние до земли.")]
    [SerializeField] private Vector3 centerOffset = Vector3.zero;

    [SerializeField] private bool followOnStart = false;

    [Tooltip("Сглаживание, позволяющее камере следовать за целью")]
    [SerializeField] private float smoothSpeed = 0.125f;
    #endregion

    // кешированная позиция камеры
    private Transform _cameraTransform;
    private bool _isFollowing;
    // кеш для смещения камеры
    private Vector3 _cameraOffSet = Vector3.zero;

    private void Start()
    {
        if (followOnStart)
        {
            OnStartFollowing();
        }
    }

    private void LateUpdate()
    {
        // Объект преобразования может не разрушиться при загрузке уровня,
        // поэтому нам нужно учитывать угловые ситуации, когда основная камера меняется каждый раз, когда мы загружаем новую сцену, и повторно подключаться, когда это происходит
        if (_cameraTransform == null && _isFollowing)
        {
            OnStartFollowing();
        }

        if (_isFollowing) Follow();
    }

    /// Запускает событие start following.
    /// Используйте это, если во время редактирования вы не знаете, за чем следить, обычно это экземпляры, управляемые сетью photon.
    public void OnStartFollowing()
    {
        _cameraTransform = Camera.main.transform;
        _isFollowing = true;
        // мы ничего не сглаживаем, мы сразу переходим к нужному кадру
        Cut();
    }

    /// <summary>
    /// Плавно следуйте к цели
    /// </summary>
    private void Follow()
    {
        _cameraOffSet.z = -distance;
        _cameraOffSet.y = height;

        _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, this.transform.position + this.transform.TransformVector(_cameraOffSet), smoothSpeed * Time.deltaTime);

        _cameraTransform.LookAt(this.transform.position + centerOffset);
    }

    private void Cut()
    {
        _cameraOffSet.z = -distance;
        _cameraOffSet.y = height;

        _cameraTransform.position = this.transform.position + this.transform.TransformVector(_cameraOffSet);
        _cameraTransform.LookAt(this.transform.position + centerOffset);
    }
}

