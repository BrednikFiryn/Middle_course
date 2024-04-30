using System;
using UnityEngine;

public class CameraWork : MonoBehaviour
{
    #region Private
    [Tooltip("��������� � ��������� ��������� x-z �� ����")]
    [SerializeField] private float distance = 0.7f;
    [Tooltip("������, �� ������� �� �����, ����� ������ ���������� ��� �����")]
    [SerializeField] private float height = 3.0f;

    [Tooltip("�������� ������ �� ��������� �� ����, ����� ����� ������ ����� � ��������� ���������� �� �����.")]
    [SerializeField] private Vector3 centerOffset = Vector3.zero;

    [SerializeField] private bool followOnStart = false;

    [Tooltip("�����������, ����������� ������ ��������� �� �����")]
    [SerializeField] private float smoothSpeed = 0.125f;
    #endregion

    // ������������ ������� ������
    private Transform _cameraTransform;
    private bool _isFollowing;
    // ��� ��� �������� ������
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
        // ������ �������������� ����� �� ����������� ��� �������� ������,
        // ������� ��� ����� ��������� ������� ��������, ����� �������� ������ �������� ������ ���, ����� �� ��������� ����� �����, � �������� ������������, ����� ��� ����������
        if (_cameraTransform == null && _isFollowing)
        {
            OnStartFollowing();
        }

        if (_isFollowing) Follow();
    }

    /// ��������� ������� start following.
    /// ����������� ���, ���� �� ����� �������������� �� �� ������, �� ��� �������, ������ ��� ����������, ����������� ����� photon.
    public void OnStartFollowing()
    {
        _cameraTransform = Camera.main.transform;
        _isFollowing = true;
        // �� ������ �� ����������, �� ����� ��������� � ������� �����
        Cut();
    }

    /// <summary>
    /// ������ �������� � ����
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

