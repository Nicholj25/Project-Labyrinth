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
    public float xMin = -3.6f;
    public float xMax = 7.7f;
    public float yMin = 0f;
    public float yMax = 2f;
    public float zMin = 0.4f;
    public float zMax = 13.75f;
    
    protected override void Awake()
    { 
        base.Awake();
        ItemRemoved = new UnityEvent();
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
            cam = cameraHandler.GetCurrentCamera();
            Vector3 mousePosition = Input.mousePosition;
            Inventory.RemoveItem(inventoryItem);
            Inventory.UnequipItem();
            rb.MovePosition(Vector3.Lerp(rb.position, cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, zPosition)), Time.deltaTime * 5));
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
            rb.freezeRotation = true;
            cam = cameraHandler.GetCurrentCamera();
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, zPosition));
            mousePosition = getInRoomPosition(mousePosition);
            rb.MovePosition(Vector3.Lerp(rb.position, mousePosition, Time.deltaTime * 5));
            rb.drag = 45;
            rb.mass = 0;
        }


    }

    /// <summary>
    /// Checks that it is within room boundaries on x axis
    /// </summary>
    /// <param name="objPosition"> float of position of axis position to check</param>
    /// <returns>True if in room boundary otherwise false </returns>
    private bool inRoomX(float objPosition)
    {
        bool inX = objPosition > xMin && objPosition < xMax;
        Debug.Log("true param InRoomX"+inX);

        return inX;
    }

    /// <summary>
    /// Checks that it is within room boundaries on y axis
    /// </summary>
    /// <param name="objPosition"> float of position of axis position to check</param>
    /// <returns>True if in room boundary otherwise false </returns>
    private bool inRoomY(float objPosition)
    {
        bool inY = objPosition > yMin && objPosition < yMax;
        Debug.Log("true param InRoomY" + inY);

        return inY;

    }

    /// <summary>
    /// Checks that it is within room boundaries on z axis
    /// </summary>
    /// <param name="objPosition"> float of position of axis position to check</param>
    /// <returns>True if in room boundary otherwise false </returns>
    private bool inRoomZ(float objPosition)
    {
        bool inZ = objPosition > zMin && objPosition < zMax;
        Debug.Log("true param InRoomZ" + inZ);

        return inZ;
    }


    /// <summary>
    /// Gets next position and keeps object in room
    /// </summary>
    /// <param name="objPosition"> Vector3 of position to check</param>
    /// <returns>Vector 3 with x y or z access unchanged if it is out of the boundary </returns>
    private Vector3 getInRoomPosition(Vector3 objPosition)
    {
        if (!inRoomX(objPosition.x))
        {
            Debug.Log(this.gameObject.transform.position);

            if (objPosition.x <= xMin)
                objPosition.x = xMin;

            if (objPosition.x >= xMax)
                objPosition.x = xMax;
        }
        if (!inRoomY(objPosition.y))
        {
            Debug.Log(this.gameObject.transform.position);
            if(objPosition.y <= yMin)
              objPosition.y = yMin;

            if (objPosition.y >= yMax)
              objPosition.y = yMax;

            }
        if (!inRoomZ(objPosition.z))
        {
            Debug.Log(this.gameObject.transform.position);

            if (objPosition.z <= zMin)
                objPosition.z = zMin;

            if (objPosition.z >= zMax)
                objPosition.z = zMax;
        }
        return objPosition;
    }
}
