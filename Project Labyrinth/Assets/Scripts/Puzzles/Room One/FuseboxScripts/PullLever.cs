using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullLever : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public CameraHandler cameraHandler;
    public TextPrompt Text;
    Animator leverAnim;
    public bool pulledAlready;
    public bool fuseAdded;

    // Start is called before the first frame update
    void Start()
    {
        leverAnim = gameObject.GetComponent<Animator>();
        pulledAlready = false;
        fuseAdded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main)
        {
            if(playerMovement.isNearby(this.gameObject) && Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);
                if (hit.transform.gameObject == this.gameObject)
                {
                    if (!fuseAdded)
                    {
                        leverPull();
                    }
                }
            }
        }
    }

    protected void leverPull()
    {
        // If the lever hasn't been pulled yet
        if (!pulledAlready)
            // Try to pull the lever, if a fuse is present in the box, pull the lever, else, don't pull lever--give negative feedback
            if (GameObject.Find("Missing Fuse 2").GetComponent<PlaceFuse1>().fuseAdded)
            // We can pull the lever successfully
            {
                leverAnim.enabled = true;
                fuseAdded = true;
                leverAnim.Play("leverPull");
                Text.UpdateTextBox("The fusebox hums to life after you pull the lever.");
                pulledAlready = true;
            }
            // Try to pull the lever without a fuse
            else{
                Text.UpdateTextBox("You pull on the lever, but as there is no fuse, nothing happens.");
                // Give some sort of ka chunk noise? Special animation?
            }
        // if it has been pulled already
        else
        {
            Text.UpdateTextBox("You've turned the power back on successfully, and decide to leave it alone. You aren't an electrician.");  
        }
    }
}
