using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZoomItem : ItemInteraction
{
    [SerializeField] private UIHandler uiHandler;
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
        if (Input.GetMouseButton(0) && Camera.main && isValidZoomConditions())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (hit.transform.gameObject == this.gameObject && !inUse)
                ActivateZoomCam();
        }
        else if (Input.GetMouseButton(0) && puzzleInput && isValidZoomConditions())
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
        uiHandler.toggleUI(false);
        inUse = false;
        puzzleInput?.Hide(puzzleInputObject);
        Cursor.lockState = CursorLockMode.Locked;
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
        uiHandler.toggleUI(true);
        puzzleInput?.Show(puzzleInputObject);
        inUse = true;
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// Checks if the player is nearby and that UI is not active
    /// 
    /// UI has to be inactive to zoom. Otherwise, if the user has
    /// UI open over zoomable object, it will zoom in and they 
    /// won't be able to get out of the inventory screen. This is
    /// because UI is active but freezes during a zoom.
    /// </summary>
    /// <returns>Bool True if valid conditions False if not</returns>
    private bool isValidZoomConditions()
    {
        if (playerMovement.isNearby(this.gameObject) && !uiHandler.isUIActive())
            return true;
        return false;
    }
}