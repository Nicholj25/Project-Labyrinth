// Source: https://gist.github.com/seferciogluecce/32c468b4392393f4f394a33a4a3e3c6a
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float speed = 1f;
    private bool isPanning = false;
    float turnY = 0;
    float turnX = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        //if (Input.GetAxis("Mouse Y") > .5)
        if (turnY + Input.GetAxis("Mouse Y") * speed <= 90 && turnY + Input.GetAxis("Mouse Y") * speed >= -90)
            turnY += Input.GetAxis("Mouse Y") * speed;
        //if (Input.GetAxis("Mouse X") > .5)
        turnX += Input.GetAxis("Mouse X") * speed;
        transform.eulerAngles = new Vector3(-turnY, turnX, 0);

        /*if (Input.GetMouseButton(0))
        {
            transform.eulerAngles += speed * new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
            isPanning = true;
        }
        else
        {
            if (isPanning == true)
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
                isPanning = false;
            }
        }*/

    }


}