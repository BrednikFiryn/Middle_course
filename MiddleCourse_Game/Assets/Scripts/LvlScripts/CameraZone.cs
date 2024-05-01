using UnityEngine;

public class CameraZone : MonoBehaviour
{
    [SerializeField] Cinemachine.CinemachineConfiner cinemachineConfiner;
    private GameObject cameraZone;

    void Start()
    {
        cameraZone = GameObject.FindGameObjectWithTag("CameraZone");
        cinemachineConfiner.m_BoundingVolume = cameraZone.GetComponent<Collider>();
    }

}
