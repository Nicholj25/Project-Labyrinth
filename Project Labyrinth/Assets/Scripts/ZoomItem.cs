using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomItem : MonoBehaviour
{
    public GameObject player;
    public GameObject mainCam;
    public GameObject zoomCam;
    public PlayerMovement playerMovement;
    private bool inUse;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player Capsule");
        mainCam = GameObject.Find("Main Camera");
        zoomCam = GameObject.Find("Desk Zoom Cam");
        playerMovement = player.GetComponent<PlayerMovement>();
        mainCam.SetActive(true);
        zoomCam.SetActive(false);
        inUse = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.isNearby(this.gameObject) && Input.GetMouseButton(0) && Camera.main)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (hit.transform.gameObject == this.gameObject && !inUse)
            {
                mainCam.SetActive(false);
                zoomCam.SetActive(true);
                playerMovement.isFrozen = true;
            }

        }
        else if (Input.GetMouseButton(1))
        {
            mainCam.SetActive(true);
            zoomCam.SetActive(false);
            playerMovement.isFrozen = false;

        }
    }

}
