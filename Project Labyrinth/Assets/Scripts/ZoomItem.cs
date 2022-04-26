using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZoomItem : ItemInteraction
{
    [SerializeField] private GameObject zoomCam;
    [SerializeField] private GameObject puzzleInputObject;
    [SerializeField] private PuzzleInput puzzleInput;
    [SerializeField] private bool inUse;
    [SerializeField] private bool hasZoomed;
    private GameObject player;
    private GameObject mainCam;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player Capsule");
        mainCam = GameObject.Find("Main Camera");
        playerMovement = player.GetComponent<PlayerMovement>();
        mainCam.SetActive(true);
        zoomCam.SetActive(false);
        inUse = false;
        hasZoomed = false;
    }


    // Update is called once per frame
    protected override void Update()
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
                puzzleInput?.Show(puzzleInputObject);
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
        inUse = false;
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
            if (!hasZoomed)
            {
                InteractionComplete?.Invoke();
                hasZoomed = true;
            }
            playerMovement.isFrozen = true;
            puzzleInput?.Show(puzzleInputObject);
            inUse = true;
        }
    }

    public Camera getCurrentCamera()
    {
        if (zoomCam && zoomCam.activeSelf)
            return zoomCam.GetComponent<Camera>();

        else if (mainCam && mainCam.activeSelf)
            return mainCam.GetComponent<Camera>();

        return null;
    }
}
