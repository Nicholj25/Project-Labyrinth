using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour
{
    public GameObject ItemModel { get; private set; }
    public string ItemName;
    public Sprite ItemImage;
    public string ItemText;
    public bool Equippable;
    public bool Inspectable;

    public PlayerInventory Inventory;

    // Start is called before the first frame update
    void Start()
    {
        ItemModel = this.gameObject;   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if(hit.transform.gameObject == this.gameObject)
            {
                Inventory.AddItem(this);
                this.gameObject.SetActive(false);
            }
        }
    }
}
