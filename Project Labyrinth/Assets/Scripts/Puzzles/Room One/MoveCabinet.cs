using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveCabinet : ItemInteraction
{
    public GameObject cabinet;
    public Transform player;
    public Transform drawerOne;
    public Transform drawerTwo;
    public Transform drawerThree;
    public Transform drawerFour;
    public UnityEvent FileCabinetKeyFound;

    public bool fileCabinetMoved;

    // Start is called before the first frame update
    protected override void Start()
    {
        fileCabinetMoved = false;
    }

    void moveCabinet()
    {
        gameObject.transform.position += new Vector3(0f, 0f, -0.75f);
        fileCabinetMoved = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (fileCabinetMoved == false)
        {
            if (drawerOne.GetComponent<CabinetDrawerToggle>().Opened &&
                drawerTwo.GetComponent<CabinetDrawerToggle>().Opened &&
                drawerThree.GetComponent<CabinetDrawerToggle>().Opened &&
                drawerFour.GetComponent<CabinetDrawerToggle>().Opened){
                moveCabinet();
                InteractionComplete?.Invoke();
            }
        }
        
    }
}