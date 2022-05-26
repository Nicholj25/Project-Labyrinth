using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableDrawer : MonoBehaviour
{
    public bool Locked;
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
           Drawer = this.transform.GetChild(0).gameObject; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main)
        {
            if (playerMovement.isNearby(this.gameObject) && Input.GetMouseButtonDown(0) && cameraHandler.IsMainCameraActive())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);
                if (hit.transform.gameObject == Drawer)
                {
                    if (!Locked)
                    {
                        // Check current state of playing animator
                        if (Animations.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                        {
                            if (Opened)
                            {
                                Opened = false;
                                Animations.Play("Close");
                            }
                            else
                            {
                                Opened = true;
                                Animations.Play("Open");
                            }
                        }
                    }
                    else
                        Text.UpdateTextBox("The drawer is locked.");
                }
            }
        }
    }
}
