using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraConfig : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook cinemachineFreeLook;
     
    private bool canRecenter;
    private const float offsetTime = 0.5f;
    private float timeToCanResetYAgain;
    private float timeToCanResetXAgain;

    private void Start()
    {
        canRecenter = true;
        timeToCanResetYAgain = cinemachineFreeLook.m_YAxisRecentering.m_RecenteringTime + cinemachineFreeLook.m_YAxisRecentering.m_WaitTime + offsetTime;
    }

    private void Update()
    {
        if (InputController.instance.isRecenteringCamera && canRecenter)
        {
            StartCoroutine(DisableRecenteringYCoroutine());
        }
    }

    private IEnumerator DisableRecenteringYCoroutine()
    {
        canRecenter = false;
        cinemachineFreeLook.m_YAxisRecentering.m_enabled = true;
        yield return new WaitForSeconds(timeToCanResetYAgain);
        cinemachineFreeLook.m_YAxisRecentering.m_enabled = false;
        canRecenter = true;
    }
}
