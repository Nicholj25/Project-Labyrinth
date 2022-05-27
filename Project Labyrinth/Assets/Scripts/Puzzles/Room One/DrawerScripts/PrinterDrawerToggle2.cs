using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for opening/closing printer desk drawers
public class PrinterDrawerToggle2 : MonoBehaviour
{
    public bool Opened { get; private set; }
    public CameraHandler cameraHandler;
    private GameObject Drawer;
    public PlayerMovement playerMovement;
    private TextPrompt text;

    /// <summary>
    /// TextPrompt script to display info
    /// </summary>
    public TextPrompt Text;

    // Start is called before the first frame update
    void Start()
    {
        Opened = false;
        // Had to add this line of code so it wouldn't throw UnityException: Transform child out of bounds
        if (this.transform.childCount > 0)
        {
           Drawer = this.gameObject; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.isNearby(this.gameObject) && Input.GetMouseButtonDown(0) && cameraHandler.IsMainCameraActive())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (hit.transform.gameObject == this.gameObject)
            {
                Text.UpdateTextBox("This drawer is glued shut.");
            }
        }
    }
}
