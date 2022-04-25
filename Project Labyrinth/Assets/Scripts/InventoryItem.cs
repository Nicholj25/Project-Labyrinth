using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour
{
    public GameObject ItemModel { get; private set; }
    public GameObject zoomCam;
    public string ItemName;
    public Sprite ItemImage;
    public string ItemText;
    public bool Equippable;
    public bool Inspectable;
    public PlayerInventory Inventory;
    public PlayerMovement playerMovement;
    private Camera cam;

    public UnityEvent PickupCompletion = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        ItemModel = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (zoomCam)
        {
            if (zoomCam.activeSelf)
            {
                cam = zoomCam.GetComponent<Camera>();
            }
        }
        else
        { 
            cam = Camera.main;
        }
        if(playerMovement.isNearby(this.gameObject) && Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if(hit.transform.gameObject == this.gameObject)
            {
                Inventory.AddItem(this);
                this.gameObject.SetActive(false);
                PickupCompletion?.Invoke();
            }
        }
    }
}
