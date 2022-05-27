using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for opening/closing printer desk drawers
public class PrinterDrawerToggle : MonoBehaviour
{
    public bool Opened { get; private set; }
    public CameraHandler cameraHandler;
    private Animator Animations;
    private GameObject Drawer;
    public PlayerMovement playerMovement;

    /// <summary>
    /// TextPrompt script to display info
    /// </summary>
    public TextPrompt Text;

    // Start is called before the first frame update
    void Start()
    {
        Opened = false;
        Animations = this.gameObject.GetComponent<Animator>();
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
            Animations.enabled = true;
            if (hit.transform.gameObject == Drawer)
            {
                // Check current state of playing animator
                if (Animations.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                {
                    Animations.enabled = true;
                    if (Opened)
                    {
                        Opened = false;
                        Animations.Play("ClosePrinter");
                    }
                    else
                    {
                        Opened = true;
                        Animations.Play("OpenPrinter");
                    }
                }
            }
        }
    }
}
