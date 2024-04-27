using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public List<GameObject> vcams;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject vcam in GameObject.FindGameObjectsWithTag("vcam"))
        {
            vcams.Add(vcam);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
