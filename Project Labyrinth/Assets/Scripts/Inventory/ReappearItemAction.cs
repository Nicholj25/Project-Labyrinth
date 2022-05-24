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
    /*
    private bool holdingItem;
    // Forward movement force.
    float moveForce = 1.0f;

    // Torque for left/right rotation.
    float rotateTorque = 1.0f;

    // Desired hovering height.
    float hoverHeight = 4.0f;

    // The force applied per unit of distance below the desired height.
    float hoverForce = 5.0f;

    // The amount that the lifting force is reduced per unit of upward speed.
    // This damping tends to stop the object from bouncing after passing over
    // something.
    float hoverDamp = 0.5f;*/
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
        if (triggerObject == collision.gameObject)
        {
            InteractionComplete?.Invoke();
            //rb.isKinematic = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (triggerObject == collision.gameObject)
        {
            Debug.Log("collision exit");
            ItemRemoved?.Invoke();
            //rb.isKinematic = true;
        }
    }

    protected override void Update()
    {
        if (Input.GetMouseButtonDown(1) && !inventoryScreen.activeSelf)// && isInRoom())
        {
            cam = cameraHandler.GetCurrentCamera();
            Vector3 mousePosition = Input.mousePosition;
            Inventory.RemoveItem(inventoryItem);
            Inventory.UnequipItem();
            //holdingItem = false;
            rb.MovePosition(Vector3.Lerp(rb.position, cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, zPosition)), Time.deltaTime * 5));
            rb.drag = startDrag;
            rb.mass = startMass;
            rb.freezeRotation = false;

            //rb.isKinematic = false;
            //rb.MovePosition(Vector3.Lerp(rb.position, cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, zPosition)), Time.deltaTime * 5));

            //rb.MovePosition(cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, zPosition)));
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
            /*if (!holdingItem)
            {
                rb.MovePosition(Vector3.Lerp(rb.position, cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, zPosition)), Time.deltaTime * 5));
                holdingItem = true;
            }
            if (Input.GetMouseButtonDown(1) && !inventoryScreen.activeSelf && isInRoom())
            {
                Inventory.RemoveItem(inventoryItem);
                Inventory.UnequipItem();
                //holdingItem = false;
                rb.MovePosition(Vector3.Lerp(rb.position, cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, zPosition)), Time.deltaTime * 5));
                rb.drag = 0;

                //rb.isKinematic = false;
                //rb.MovePosition(Vector3.Lerp(rb.position, cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, zPosition)), Time.deltaTime * 5));

                //rb.MovePosition(cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, zPosition)));
            }
            else
            {*/
            //rb.isKinematic = true;

            //rb.MovePosition(Vector3.Lerp(rb.position, cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, zPosition)), Time.deltaTime * 5));
            rb.MovePosition(Vector3.Lerp(rb.position, mousePosition, Time.deltaTime * 5));
            rb.drag = 45;
            rb.mass = 0;
            
                /*hoverHeight = cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, zPosition)).y;

                // Push the object based on arrow key input.
                rb.AddForce(Input.GetAxis("Vertical") * moveForce * transform.forward);
                rb.AddForce(Input.GetAxis("Horizontal") * moveForce * transform.right);

                RaycastHit hit;
                Ray downRay = new Ray(transform.position, -Vector3.up);

                // Cast a ray straight downwards.
                if (Physics.Raycast(downRay, out hit))
                {
                    // The "error" in height is the difference between the desired height
                    // and the height measured by the raycast distance.
                    float hoverError = hoverHeight - hit.distance;

                    // Only apply a lifting force if the object is too low (ie, let
                    // gravity pull it downward if it is too high).
                    if (hoverError > 0)
                    {
                        // Subtract the damping from the lifting force and apply it to
                        // the rigidbody.
                        float upwardSpeed = rb.velocity.y;
                        float lift = hoverError * hoverForce - upwardSpeed * hoverDamp;
                        rb.AddForce(lift * Vector3.up);
                    }
                }*/
                //rb.MovePosition(cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, zPosition)));

            //}

        }


    }

    /*private bool isInRoom()
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
    }*/

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
