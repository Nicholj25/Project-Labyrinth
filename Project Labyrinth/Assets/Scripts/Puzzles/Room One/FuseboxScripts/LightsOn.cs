using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOn : MonoBehaviour
{
    public GameObject cabinet;
    public Transform player;
    public Transform drawerOne;
    public Transform drawerTwo;
    public Transform drawerThree;
    public Transform drawerFour;

    public bool lightsOn;

    // Start is called before the first frame update
    void Start()
    {
        lightsOn = false;
    }

    void turnLightsOn()
    {
        // turn on all lights in room
        // turn on projector
        gameObject.transform.position += new Vector3(0f, 0f, -0.75f);
        lightsOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (lightsOn == false)
        {
            // do if fusebox 1.pulledAlready &&
            // if fusebox 2.pulledAlready &&
            // if fusebox 3.pulledAlready
            // turnLightsOn
            if (drawerOne.GetComponent<CabinetDrawerToggle>().Opened &&
                drawerTwo.GetComponent<CabinetDrawerToggle>().Opened &&
                drawerThree.GetComponent<CabinetDrawerToggle>().Opened &&
                drawerFour.GetComponent<CabinetDrawerToggle>().Opened){
                turnLightsOn();
            }
        }
        
    }
}
