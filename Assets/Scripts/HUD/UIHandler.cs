using UnityEngine;

public class UIHandler : MonoBehaviour
{
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
        if (Input.GetKeyDown(KeyCode.I))
        {
            Normal.gameObject.SetActive(!Normal.activeSelf);
            Inventory.gameObject.SetActive(!Inventory.gameObject.activeSelf);
            Inventory.PopulateScreen();
        }

    }
}
