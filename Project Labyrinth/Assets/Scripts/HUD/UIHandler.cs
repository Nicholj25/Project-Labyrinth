using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private InventoryScreen inventoryUI;
    [SerializeField] private HintText hintUI;
    [SerializeField] private GameObject instructions;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject textBackground;
    public GameObject Normal;
    public InventoryScreen Inventory;
    public Timer Time;

    // Start is called before the first frame update
    void Start()
    {
        // Set all to active to trigger individual Start() functions
        Inventory.gameObject.SetActive(true);
        Normal.SetActive(true);
        Time.gameObject.SetActive(true);

        // Disable Inventory screen until needed
        Inventory.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !Inventory.isFrozen)
        {
            Normal.gameObject.SetActive(!Normal.activeSelf);
            Inventory.gameObject.SetActive(!Inventory.gameObject.activeSelf);
            Inventory.PopulateScreen();
        }

    }

    /// <summary>
    /// Toggles UI on and off
    /// </summary>
    /// <param name="state">False == off; True == On</param>
    public void toggleUI(bool state)
    {
        inventoryUI.isFrozen = state;
        hintUI.isFrozen = state;
        instructions.SetActive(!state);
    }

    public bool isUIActive()
    {
        if (inventory.activeSelf || textBackground.activeSelf)
        {
            return true;
        }
        return false;
    }
}
