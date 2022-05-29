using UnityEngine;
using UnityEngine.Events;

public class InventoryItem : MonoBehaviour
{
    public GameObject ItemModel { get; private set; }
    public GameObject zoomCam;
    public GameObject mainCam;
    public CameraHandler cameraHandler;
    public CursorHoverEffect cursorHover;
    public string ItemName;
    public Sprite ItemImage;
    public string ItemText;
    public bool Equippable;
    public bool Inspectable;
    public bool Reappearable;
    public PlayerInventory Inventory;
    public PlayerMovement playerMovement;
    private Camera cam;
    [SerializeField] private ZoomItem zoomItem;

    public UnityEvent PickupCompletion = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        cursorHover = gameObject.AddComponent<CursorHoverEffect>();
        ItemModel = this.gameObject;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        cam = cameraHandler.GetCurrentCamera();

        if(Input.GetMouseButtonDown(0))
        {
            if (cam != null)
            {
                Vector3 mousePosition = Input.mousePosition;
                Ray ray = cam.ScreenPointToRay(mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);
                if (playerMovement.isNearby(this.gameObject) && hit.transform.gameObject == this.gameObject)
                {
                    if (!Inventory.ContainsItem(this))
                    {
                        Inventory.AddItem(this);
                        this.gameObject.SetActive(false);
                        PickupCompletion?.Invoke();
                    }
                }
            }
        }
    }
}
