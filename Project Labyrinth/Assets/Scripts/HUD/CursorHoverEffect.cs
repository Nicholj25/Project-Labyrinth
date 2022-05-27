using UnityEngine;
using UnityEngine.UI;

public class CursorHoverEffect : MonoBehaviour
{
    public Sprite interactionTexture;
    public PlayerInventory playerInventory;
    public PlayerMovement player;
    public Image cursor;
    public bool isOn;
    public float width = 50f;
    public float height = 30f;
    private Sprite defaultTexture;
    private InventoryItem inventoryItem;

    // Sources: https://forum.unity.com/threads/resources-load-not-working.95527/
    public void Start()
    {
        isOn = true;
        inventoryItem = GetComponent<InventoryItem>();
        inventoryItem?.PickupCompletion.AddListener(OnMouseExit);
        interactionTexture = Resources.Load<Sprite>("Sprites/eye_cursor");
        player = GameObject.Find("Player Capsule").GetComponent<PlayerMovement>();
        playerInventory = GameObject.Find("Player Capsule").GetComponent<PlayerInventory>();
        cursor = GameObject.Find("Cursor")?.GetComponent<Image>();
        defaultTexture = cursor?.sprite;
    }

    /// <summary>
    /// Changes cursor icon to match tag
    /// </summary>
    private void OnMouseEnter()
    {
        if (!playerInventory.CurrentItem && isOn && player.isNearby(this.gameObject))
        {
            if (gameObject.tag == "Interaction")
            {
                cursor.sprite = interactionTexture;
                cursor.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            }
        }
    }

    /// <summary>
    /// Changes cursor icon back to the default
    /// </summary>
    private void OnMouseExit()
    {
        cursor.sprite = defaultTexture;
        cursor.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(10, 10);
    }


}
