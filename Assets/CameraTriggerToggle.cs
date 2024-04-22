using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerToggle : MonoBehaviour
{
    public Transform parent;

    private void OnTriggerEnter(Collider other)
    {
        parent.GetComponent<CameraSwitcher>().SwitchCamera();
    }
}
