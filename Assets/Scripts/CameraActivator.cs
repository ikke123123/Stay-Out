using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraActivator : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private CinemachineVirtualCamera cinemachineCamera = null;
    [SerializeField] private bool enable = false;
    [SerializeField] private bool disable = false;
    [SerializeField] private int highPriority = 15;
    [SerializeField] private int lowPriority = 5;

    private void Start()
    {
        cinemachineCamera.m_Priority = lowPriority;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enable == true)
        {
            cinemachineCamera.m_Priority = highPriority;
        }
        if (disable == true)
        {
            cinemachineCamera.m_Priority = lowPriority;
        }
    }
}
