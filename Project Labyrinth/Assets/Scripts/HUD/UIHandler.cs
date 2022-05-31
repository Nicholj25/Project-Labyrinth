using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private InventoryScreen inventoryUI;
    [SerializeField] private HintText hintUI;
    [SerializeField] private GameObject instructions;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject textBackground;
    [SerializeField] private PlayerCam playerCam;
    private PlayerMovement playerMovement;
    public GameObject Normal;
    public InventoryScreen Inventory;
    public Timer Time;

    // Start is called before the first frame update
    void Start()
    {
        // Start with locked cursor
        Cursor.lockState = CursorLockMode.Locked;
        
        // Set all to active to trigger individual Start() functions
        Inventory.gameObject.SetActive(true);
        Normal.SetActive(true);
        Time.gameObject.SetActive(true);

        // Disable Inventory screen until needed
        Inventory.gameObject.SetActive(false);

        playerMovement = playerCam.gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !Inventory.isFrozen)
        {
            SwapScreens();
        }

    }

    public void SwapScreens()
    {
        Normal.gameObject.SetActive(!Normal.activeSelf);
        Inventory.gameObject.SetActive(!Inventory.gameObject.activeSelf);

        Inventory.UpdateSelected();
        Inventory.PopulateScreen();

        // Change cursor and lock camera when inventory menu is open
        Cursor.lockState = Inventory.gameObject.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        playerCam.IsFrozen = Inventory.gameObject.activeSelf;
        playerMovement.isFrozen = Inventory.gameObject.activeSelf;
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
        if (inventory.activeSelf)
        {
            return true;
        }
        return false;
    }
}
