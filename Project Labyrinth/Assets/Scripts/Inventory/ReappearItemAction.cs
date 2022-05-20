using UnityEngine;

public class ReappearItemAction : ItemInteraction
{
    private Rigidbody rb;
    public float zPosition = 1f;
    public InventoryItem inventoryItem;
    public GameObject triggerObject;
    public GameObject inventoryScreen;
    void Start()
    {
        inventoryItem = GetComponent<InventoryItem>();
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    /// <summary>
    /// Triggers the InteractionComplete event when item collides with the trigger object
    /// </summary>
    /// <param name="collision">Unity collision object</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (triggerObject == collision.gameObject)
        {
            InteractionComplete?.Invoke();
            rb.isKinematic = true;
        }

    }

    // Update is called once per frame
    protected override void Update()
    {
        if (Inventory.CurrentItem == inventoryItem)
        {
            cam = cameraHandler.GetCurrentCamera();

            Vector3 mousePosition = Input.mousePosition;

            if (Input.GetMouseButtonDown(1) && !inventoryScreen.activeSelf && isInRoom())
            {
                Inventory.RemoveItem(inventoryItem);
                Inventory.UnequipItem();
                rb.isKinematic = false;
                rb.MovePosition(cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, zPosition)));
            }
            else
            {
                rb.isKinematic = true;
                rb.MovePosition(cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, zPosition)));
            }

        }


    }

    private bool isInRoom()
    {
        bool inX = this.gameObject.transform.position.x >  -3.6 && this.gameObject.transform.position.x < 7.75;
        bool inY = this.gameObject.transform.position.y > 0.01 && this.gameObject.transform.position.y < 2;
        bool inZ = this.gameObject.transform.position.z > 0.3 && this.gameObject.transform.position.z < 13.75;

        if (inX && inY && inZ)
        {
            Debug.Log("true isInRoom");
            Debug.Log(this.gameObject.transform.position);
            return true;
        }
        else
        {
            Debug.Log("false isInRoom");
            Debug.Log(this.gameObject.transform.position);
            return false;
        }
    }
}
