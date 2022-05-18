using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialOnOff : MonoBehaviour
{
     // The material we want to click on and off
    public Material material1;
    bool materialOn = true;

    void Start () 
    {
        GetComponent<Renderer>().material = material1;
    }
    
    //void OnMouseDown()
    //{
        //if (materialOn == true)
        //{
            //material1.enabled = false;
            //materialOn = false;
        //}
        //else
        //{
            //material1.enabled = true;
            //materialOn = true;
        //}
    //}
}
