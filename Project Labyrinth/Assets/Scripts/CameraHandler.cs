using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public List<GameObject> zoomCamObj;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Camera GetCurrentCamera()
    {
        for (int i = 0; i < zoomCamObj.Count; i++)
        { 
            if(zoomCamObj[i].gameObject.activeSelf == true)
                return zoomCamObj[i].gameObject.GetComponent<Camera>();
        }
        return Camera.main;
    }

    public bool IsMainCameraActive()
    {
        return GetCurrentCamera() == Camera.main;

    }
}
