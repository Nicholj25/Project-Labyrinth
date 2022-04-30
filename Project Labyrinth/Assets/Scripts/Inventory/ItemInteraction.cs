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

    // Start is called before the first frame update
    void Start()
    {
        InteractionComplete = new UnityEvent();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (playerMovement.isNearby(this.gameObject) && Input.GetMouseButtonDown(0))
        {
            // Make sure not activating without being tied to main camera
            if(Camera.main)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);
                if (hit.transform.gameObject == this.gameObject && AcceptableItems.Contains(Inventory.CurrentItem))
                {
                    ItemUsageAction();
                }
            }
        }
    }
}
