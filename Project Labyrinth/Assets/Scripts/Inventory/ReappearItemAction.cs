using UnityEngine;
using UnityEngine.Events;


public class ReappearItemAction : ItemInteraction
{
    private Rigidbody rb;
    private float startDrag;
    private float startMass;
    public float zPosition = 1f;
    public InventoryItem inventoryItem;
    public GameObject triggerObject;
    public GameObject inventoryScreen;
    public UnityEvent ItemRemoved;
    public UnityEvent ItemEquipped;
    public float xMin = -3.6f;
    public float xMax = 7.7f;
    public float yMin = 0f;
    public float yMax = 2f;
    public float zMin = 0.4f;
    public float zMax = 13.7f;
    
    protected override void Awake()
    { 
        base.Awake();
        ItemRemoved = new UnityEvent();
        ItemEquipped = new UnityEvent();
    }
    void Start()
    {
        inventoryItem = GetComponent<InventoryItem>();
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        startDrag = rb.drag;
    }

    /// <summary>
    /// Triggers the InteractionComplete event when item collides with the trigger object
    /// </summary>
    /// <param name="collision">Unity collision object</param>
    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector3.zero;
        if (triggerObject)
        {
            if (triggerObject == collision.gameObject)
            {
                InteractionComplete?.Invoke();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (triggerObject)
        {
            if (triggerObject == collision.gameObject)
            {
                ItemRemoved?.Invoke();
            }
        }
    }

    protected override void Update()
    {
        if (Input.GetMouseButtonDown(1) && !inventoryScreen.activeSelf)
        {
            Inventory.RemoveItem(inventoryItem);
            Inventory.UnequipItem();
            rb.drag = startDrag;
            rb.mass = startMass;
            rb.freezeRotation = false;
        }
    }
    // Update is called once per frame
    protected void FixedUpdate()
    {

        if (Inventory.CurrentItem == inventoryItem)
        {
            ItemEquipped?.Invoke();
            rb.freezeRotation = true;
            cam = cameraHandler.GetCurrentCamera();
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, zPosition));
            mousePosition = GetValidPosition(mousePosition, new Vector3(xMin, yMin, zMin), new Vector3(xMax, yMax, zMax));
            rb.MovePosition(Vector3.Lerp(rb.position, mousePosition, Time.deltaTime * 5));
            rb.drag = 45;
            rb.mass = 0;
        }


    }

    /// <summary>
    /// Checks that it is within room boundaries on x axis
    /// </summary>
    /// <param name="objPosition"> float of position of axis position to check</param>
    /// <param name="xMini"> float of minimum x boundary</param>
    /// <param name="xMaxi"> float of maximum x boundary</param>
    /// <returns>True if in room boundary otherwise false </returns>
    private bool inRoomX(float objPosition, float xMini, float xMaxi)
    {
        bool inX = objPosition > xMini && objPosition < xMaxi;

        return inX;
    }

    /// <summary>
    /// Checks that it is within room boundaries on y axis
    /// </summary>
    /// <param name="objPosition"> float of position of axis position to check</param>
    /// <param name="yMini"> float of minimum y boundary</param>
    /// <param name="yMaxi"> float of maximum y boundary</param>
    /// <returns>True if in room boundary otherwise false </returns>
    private bool inRoomY(float objPosition, float yMini, float yMaxi)
    {
        bool inY = objPosition > yMini && objPosition < yMaxi;

        return inY;

    }

    /// <summary>
    /// Checks that it is within room boundaries on z axis
    /// </summary>
    /// <param name="objPosition"> float of position of axis position to check</param>
    /// <param name="zMini"> float of minimum z boundary</param>
    /// <param name="zMaxi"> float of maximum z boundary</param>
    /// <returns>True if in room boundary otherwise false </returns>
    private bool inRoomZ(float objPosition, float zMini, float zMaxi)
    {
        bool inZ = objPosition > zMini && objPosition < zMaxi;

        return inZ;
    }


    /// <summary>
    /// Gets next position and keeps object in room
    /// </summary>
    /// <param name="objPosition"> Vector3 of position to check</param>
    /// <returns>Vector 3 with x y or z access unchanged if it is out of the boundary </returns>
    public Vector3 GetValidPosition(Vector3 objPosition, Vector3 minPosition, Vector3 maxPosition)
    {
        if (!inRoomX(objPosition.x, minPosition.x, maxPosition.x))
        {
            Debug.Log(this.gameObject.transform.position);

            if (objPosition.x <= minPosition.x)
                objPosition.x = minPosition.x;

            if (objPosition.x >= maxPosition.x)
                objPosition.x = maxPosition.x;
        }
        if (!inRoomY(objPosition.y, minPosition.y, maxPosition.y))
        {
            Debug.Log(this.gameObject.transform.position);
            if(objPosition.y <= minPosition.y)
              objPosition.y = minPosition.y;

            if (objPosition.y >= maxPosition.y)
              objPosition.y = maxPosition.y;

            }
        if (!inRoomZ(objPosition.z, minPosition.z, maxPosition.z))
        {
            Debug.Log(this.gameObject.transform.position);

            if (objPosition.z <= minPosition.z)
                objPosition.z = minPosition.z;

            if (objPosition.z >= maxPosition.z)
                objPosition.z = maxPosition.z;
        }
        return objPosition;
    }
}
