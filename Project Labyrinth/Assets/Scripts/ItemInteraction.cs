using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemInteraction : MonoBehaviour
{
    public InventoryItem Item;
    public List<InventoryItem> AcceptableItems;
    public PlayerMovement playerMovement;
    public CameraHandler cameraHandler;
    public Camera cam;

    /// <summary>
    /// TextPrompt script to display info
    /// </summary>
    public TextPrompt Text;

    /// <summary>
    /// The players inventory. Used to check currently equipped item.
    /// </summary>
    public PlayerInventory Inventory;

    /// <summary>
    /// Used to trigger events after interaction is complete
    /// </summary>
    public UnityEvent InteractionComplete;

    protected virtual void ItemUsageAction() { }

    void Awake()
    {
        cameraHandler = GameObject.Find("Main Camera").GetComponent<CameraHandler>();
        InteractionComplete = new UnityEvent();
        cam = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        cam = cameraHandler.GetCurrentCamera();
        if (playerMovement.isNearby(this.gameObject) && Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (hit.transform.gameObject == this.gameObject && AcceptableItems.Contains(Inventory.CurrentItem))
            {
                ItemUsageAction();
            }
        }
    }
}
