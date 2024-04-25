using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraTriggerToggle : MonoBehaviour
{
    public Transform parent;

    private bool inRange = false;

    public GameObject InspectionCanvas;

    public UnityEvent OnInspect;

    private void OnTriggerEnter(Collider other)
    {
        inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
    }

    private void Update()
    {
        if (inRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                parent.GetComponent<CameraSwitcher>().SwitchCamera();

                GameObject inspectCanvas = Instantiate(InspectionCanvas);

                OnInspect.Invoke();
            }
        }
    }
}
