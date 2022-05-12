using UnityEngine;

public class ReappearItemAction : ItemInteraction
{
    private Rigidbody rb;
    public float zPosition = 1f;
    public InventoryItem inventoryItem;
    public GameObject triggerObject;
    void Start()
    {
        inventoryItem = GetComponent<InventoryItem>();
        rb = GetComponent<Rigidbody>();
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
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (Inventory.CurrentItem == inventoryItem)
        {
            cam = cameraHandler.GetCurrentCamera();

            Vector3 mousePosition = Input.mousePosition;

            if (Input.GetMouseButtonDown(1))
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
}
