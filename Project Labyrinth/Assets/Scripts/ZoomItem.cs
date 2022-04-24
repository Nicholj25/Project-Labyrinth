using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomItem : MonoBehaviour
{
    public GameObject zoomCam;
    public GameObject puzzleInputObject;
    public PuzzleInput puzzleInput;
    public PlayerMovement playerMovement;
    private bool inUse;
    private GameObject player;
    private GameObject mainCam;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player Capsule");
        mainCam = GameObject.Find("Main Camera");
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
            ActivateZoomCam();

        }
        else if (playerMovement.isNearby(this.gameObject) && Input.GetMouseButton(0) && puzzleInput)
        {
            // Left Click Brings Back Puzzle Input Window If Exists and Is Closed
            if (!puzzleInput.puzzleInputWindow.activeSelf)
            {
                puzzleInput.Show(puzzleInputObject);
            }
        }
        else if (Input.GetMouseButton(1))
        {
            ZoomOut();
        }
    }

    public void ZoomOut()
    {
        mainCam.SetActive(true);
        zoomCam.SetActive(false);
        playerMovement.isFrozen = false;
        if (puzzleInput)
        {
            puzzleInput.Hide(puzzleInputObject);
        }
    }

    private void ActivateZoomCam()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if (hit.transform.gameObject == this.gameObject && !inUse)
        {
            mainCam.SetActive(false);
            zoomCam.SetActive(true);
            playerMovement.isFrozen = true;
            if (puzzleInput)
            {
                puzzleInput.Show(puzzleInputObject);
            }
        }
    }
}
