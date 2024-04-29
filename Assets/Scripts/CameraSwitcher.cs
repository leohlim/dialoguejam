using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;

    public CinemachineVirtualCamera mainCam;

    private CameraManager camManager;

    private int basePriority = 10;

    private void Start()
    {
        camManager = GameObject.FindGameObjectWithTag("Camera Manager").GetComponent<CameraManager>();
    }
    public void SwitchCamera()
    {
        foreach (GameObject gam in camManager.vcams)
        {
            gam.GetComponent<CinemachineVirtualCamera>().Priority = basePriority;
        }

        vcam.Priority = basePriority + 1;
    }

    public void SwitchCameraMain()
    {
        foreach(GameObject gam in camManager.vcams)
        {
            gam.GetComponent<CinemachineVirtualCamera>().Priority = basePriority;
        }

        mainCam.Priority = basePriority + 1;
    }
}
