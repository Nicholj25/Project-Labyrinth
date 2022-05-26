using UnityEngine;
using UnityEngine.UI;

public class CursorHoverEffect : MonoBehaviour
{
    public Sprite interactionTexture;
    public PlayerInventory playerInventory;
    public Image cursor;
    public float width = 50f;
    public float height = 30f;
    public bool isOn;
    private Sprite defaultTexture;
    private InventoryItem inventoryItem;

    // Start is called before the first frame update
    void Start()
    {
        //width = 50;
        //height = 30;
        isOn = true;
        defaultTexture = cursor.sprite;
        inventoryItem = GetComponent<InventoryItem>();
        inventoryItem?.PickupCompletion.AddListener(OnMouseExit);

    }

    /// <summary>
    /// Changes cursor icon to match tag
    /// </summary>
    private void OnMouseEnter()
    {
        if (isOn)
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
