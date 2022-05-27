using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceFuse2 : ItemInteraction
{
    /// <summary>
    /// Animator to use when placing a fuse
    /// </summary>
    private Animator placeFuse;
    public bool fuseAdded;

    protected override void ItemUsageAction() 
    {
        // Moving fuse to correct location
        Inventory.CurrentItem.transform.parent = this.transform;
        Inventory.CurrentItem.gameObject.SetActive(true);
        fuseAdded = true;

        // Play placeFuse animation
        Inventory.CurrentItem.transform.localPosition = new Vector3();
        Inventory.CurrentItem.transform.localRotation = new Quaternion();
        placeFuse.enabled = true;
        placeFuse.Play("placeFuse3");

        // Remove fuse from inventory
        Inventory.RemoveItem(Inventory.CurrentItem);
        Inventory.UnequipItem();

        InteractionComplete?.Invoke();
    }
    protected override void Awake()
    {
        cam = Camera.main;

    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        placeFuse = this.GetComponent<Animator>();
        fuseAdded = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (Camera.main){
            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);
                if (hit.transform.gameObject == this.gameObject)
                {
                    if (Item == Inventory.CurrentItem)
                    {
                        ItemUsageAction();
                    }
                    else
                    {
                        Text.UpdateTextBox("You're not holding an item that fits in the slot.");
                    }
                }
            }
        }  
    }
}

