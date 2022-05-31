using UnityEngine;
using UnityEngine.UI;

public class CursorHoverEffect : MonoBehaviour
{
    public Sprite interactionTexture;
    public Sprite zoomTexture;
    public Sprite inventoryTexture;
    public PlayerInventory playerInventory;
    public PlayerMovement player;
    public Image cursor;
    public bool isOn = true;
    public Vector2 interactionSize;
    public Vector2 zoomSize;
    public Vector2 inventorySize;
    private bool isHovering;
    private Sprite defaultTexture;
    private InventoryItem inventoryItem;
    bool reappearEquipped = false;


    // Sources: https://forum.unity.com/threads/resources-load-not-working.95527/
    public void Start()
    {
        interactionSize = new Vector2(50f, 30f);
        zoomSize = new Vector2(50f, 30f);
        inventorySize = new Vector2(60f, 36f);

        isHovering = false;
        inventoryItem = GetComponent<InventoryItem>();
        inventoryItem?.PickupCompletion.AddListener(OnMouseExit);
        interactionTexture = Resources.Load<Sprite>("Sprites/eye_cursor");
        zoomTexture = Resources.Load<Sprite>("Sprites/eye_cursor");
        inventoryTexture = Resources.Load<Sprite>("Sprites/diamond_cursor");
        player = GameObject.Find("Player Capsule").GetComponent<PlayerMovement>();
        playerInventory = GameObject.Find("Player Capsule").GetComponent<PlayerInventory>();
        cursor = GameObject.Find("Cursor")?.GetComponent<Image>();
        defaultTexture = cursor?.sprite;
    }
    private void Update()
    {
        if (isHovering && player.isNearby(this.gameObject))
            OnMouseEnter();
    }

    /// <summary>
    /// Changes cursor icon to match tag
    /// </summary>
    private void OnMouseEnter()
    {
        isHovering = true;
        if (!cursor)
            Start();
        if (inventoryItem)
            reappearEquipped = inventoryItem.Reappearable && playerInventory.CurrentItem == inventoryItem;
        if ((!reappearEquipped || !inventoryItem.Reappearable) && isOn && player.isNearby(this.gameObject))
        {

            if (gameObject.tag == "Interaction")
            {
                cursor.sprite = interactionTexture;
                cursor.gameObject.GetComponent<RectTransform>().sizeDelta = interactionSize;
            }
            else if (gameObject.tag == "Zoom")
            {
                cursor.sprite = zoomTexture;
                cursor.gameObject.GetComponent<RectTransform>().sizeDelta = zoomSize;
            }
            else if (gameObject.tag == "Inventory")
            {
                cursor.sprite = inventoryTexture;
                cursor.gameObject.GetComponent<RectTransform>().sizeDelta = inventorySize;
            }

        }
    }

    /// <summary>
    /// Changes cursor icon back to the default
    /// </summary>
    private void OnMouseExit()
    {
        isHovering = false;
        cursor.sprite = defaultTexture;
        cursor.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(10, 10);
    }


}
