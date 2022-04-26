using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZoomItem : ItemInteraction
{
    [SerializeField] private InventoryScreen inventoryUI;
    [SerializeField] private HintText hintUI;
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

    private void Start()
    {
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (playerMovement.isNearby(this.gameObject) && Input.GetMouseButton(0) && Camera.main)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (hit.transform.gameObject == this.gameObject && !inUse)
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

    /// <summary>
    /// Changes from Zoom Camera to Main Camera
    /// </summary>
    public void ZoomOut()
    {
        mainCam.SetActive(true);
        zoomCam.SetActive(false);
        playerMovement.isFrozen = false;
        inventoryUI.isFrozen = false;
        hintUI.isFrozen = false;
        inUse = false;
        puzzleInput?.Hide(puzzleInputObject);
    }
    /// <summary>
    /// Changes From Main Camera to Zoom Camera
    /// </summary>
    private void ActivateZoomCam()
    { 

        mainCam.SetActive(false);
        zoomCam.SetActive(true);
        if (!hasZoomed)
        {
            InteractionComplete?.Invoke();
            hasZoomed = true;
        }
        playerMovement.isFrozen = true;
        inventoryUI.isFrozen = true;
        hintUI.isFrozen = true;
        puzzleInput?.Show(puzzleInputObject);
        inUse = true;
    }

    /// <summary>
    /// Gets the Camera that is Currently Active
    /// </summary>
    /// <returns>Currently Active Camera or Null if
    /// another zoom camera other than zoomCam is active</returns>
    public Camera getCurrentCamera()
    {
        if (zoomCam && zoomCam.activeSelf)
            return zoomCam.GetComponent<Camera>();

        else if (mainCam && mainCam.activeSelf)
            return mainCam.GetComponent<Camera>();

        return null;
    }
}
