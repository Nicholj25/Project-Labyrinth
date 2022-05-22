using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryScreen : MonoBehaviour
{
    /// <summary>
    /// used to turn off Inventory Screen to allow for text entry use of I key
    /// </summary>
    public bool isFrozen { set; get; }

    public PlayerInventory Inventory;
    public GameObject InventorySelection { get; private set; }
    public GameObject ItemDescriptionTextBox { get; private set; }
    public Button EquipButton { get; private set; }

    /// <summary>
    /// Index of the last item in the inventory clicked on
    /// </summary>
    private int CurrentSelectedIndex;

    // Start is called before the first frame update
    void Awake()
    {
        isFrozen = false;
        InventorySelection = this.transform.GetChild(1).gameObject;
        ItemDescriptionTextBox = this.transform.GetChild(2).gameObject;
        
        EquipButton = this.transform.GetChild(3).GetChild(0).GetComponent<Button>();
        EquipButton.interactable = false;
    }


    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Updates the inventory screen when opening and closing
    /// </summary>
    /// <param name="open">The screen been opened (T) The screen has been closed (F)</param>
    public void PopulateScreen()
    {

        for (int i = 0; i <= 7; i++)
        {
            // Clear Inventory Screen
            GameObject currentInventoryButton = InventorySelection.transform.GetChild(i).gameObject;

            // Clear Text
            currentInventoryButton.transform.GetChild(0).GetComponentInChildren<Text>().text = "";
            ItemDescriptionTextBox.transform.GetComponentInChildren<Text>().text = "";
            currentInventoryButton.transform.GetChild(2).gameObject.SetActive(false);


            // Update Image
            Image currentImage = currentInventoryButton.transform.GetChild(1).GetChild(0).GetComponentInChildren<Image>();
            Color currentColor = currentImage.color;
            currentColor.a = 0f;
            currentImage.color = currentColor;

            // Disable buttons
            currentInventoryButton.GetComponent<Button>().interactable = false;
            if (i == 0)
            {
                EquipButton.interactable = false;
                EquipButton.GetComponentInChildren<Text>().text = "Equip";
            }

            if (Inventory.HeldItems.Count > i)
            {
                currentInventoryButton = InventorySelection.transform.GetChild(i).gameObject;

                // Update Text
                currentInventoryButton.transform.GetChild(0).GetComponentInChildren<Text>().text = Inventory.HeldItems[i].ItemName;

                // Update Image
                currentImage = currentInventoryButton.transform.GetChild(1).GetChild(0).GetComponentInChildren<Image>();
                currentImage.sprite = Inventory.HeldItems[i].ItemImage;
                currentImage.preserveAspect = true;
                currentColor = currentImage.color;
                currentColor.a = 1f;
                currentImage.color = currentColor;

                // Update Equipped
                currentInventoryButton.transform.GetChild(2).gameObject.SetActive(Inventory.CurrentItem == Inventory.HeldItems[i]);


                if (Inventory.CurrentItem)
                {
                    EquipButton.interactable = true;
                    if (Inventory.CurrentItem == Inventory.HeldItems[i])
                    {
                        EquipButton.GetComponentInChildren<Text>().text = "Unequip";
                    }
                }
                else
                {   
                    // Enable buttons
                    currentInventoryButton.GetComponent<Button>().interactable = true;
                }
            }

        }
    }

    public void ChooseObject()
    {
        CurrentSelectedIndex = EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex();

        // Update Item Text
        ItemDescriptionTextBox.transform.GetComponentInChildren<Text>().text = Inventory.HeldItems[CurrentSelectedIndex].ItemText;

        // Make equip button selectable
        EquipButton.interactable = true;

        if (Inventory.CurrentItem == Inventory.HeldItems[CurrentSelectedIndex])
            EquipButton.GetComponentInChildren<Text>().text = "Unequip";
        else
            EquipButton.GetComponentInChildren<Text>().text = "Equip";
    }

    public void EquipObject()
    {
        if(Inventory.CurrentItem == Inventory.HeldItems[CurrentSelectedIndex])
        {
            if (Inventory.CurrentItem.Reappearable)
                Inventory.ToggleItem(Inventory.CurrentItem);
            Inventory.UnequipItem();
                
            EquipButton.GetComponentInChildren<Text>().text = "Equip";
        }
        else
        {
            Inventory.EquipItem(Inventory.HeldItems[CurrentSelectedIndex]);
            EquipButton.GetComponentInChildren<Text>().text = "Unequip";
        }

        // Update
        PopulateScreen();
    }
}
