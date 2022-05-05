using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour
{
    public GameObject ItemModel { get; private set; }
    public GameObject zoomCam;
    public GameObject mainCam;
    public CameraHandler cameraHandler;
    public string ItemName;
    public Sprite ItemImage;
    public string ItemText;
    public bool Equippable;
    public bool Inspectable;
    public bool Reappearable;
    //public bool FollowCursor;
    public PlayerInventory Inventory;
    public PlayerMovement playerMovement;
    public float zPosition;
    private Camera cam;
    [SerializeField] private ZoomItem zoomItem;

    public UnityEvent PickupCompletion = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        ItemModel = this.gameObject;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        cam = cameraHandler.GetCurrentCamera();

        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = cam.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (playerMovement.isNearby(this.gameObject) && hit.transform.gameObject == this.gameObject)
            {
                Inventory.AddItem(this);
                this.gameObject.SetActive(false);
                PickupCompletion?.Invoke();
            }
            else if (Inventory.ContainsItem(this) && Reappearable)
            {
                /*if (!FollowCursor)
                {*/
                    Debug.Log(this + "is in inventory");
                 //   this.gameObject.transform.position = cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, zPosition));
               /*     FollowCursor = true;
                }
                else if (FollowCursor)
                {
                    this.gameObject.transform.position = cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, zPosition));
                   FollowCursor = false;
                 }*/

            }

        }
    }
}
