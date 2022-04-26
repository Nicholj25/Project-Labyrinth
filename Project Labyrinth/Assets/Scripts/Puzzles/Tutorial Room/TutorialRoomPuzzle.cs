using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRoomPuzzle : Puzzle
{
    public BookOnShelfAction Trigger;
    public OpenableDrawer LockedDrawer;
    public TextPrompt Text;
    public InventoryItem Key;

    private bool BookOnShelf;

    // Start is called before the first frame update
    void Start()
    {
        Trigger.InteractionComplete.AddListener(UnlockDrawer);
        Key.PickupCompletion.AddListener(() => { KeyObtained = true; Completed = true; });

        // Start with key unobtained and book not on shelf
        BookOnShelf = false;
        KeyObtained = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UnlockDrawer()
    {
        LockedDrawer.Locked = false;
        BookOnShelf = true;
        Text.UpdateTextBox("Returning the book to the shelf has caused a clicking sound to be heard from the desk");
    }

    public override string GetCurrentHint()
    {
        string hintText = "";

        if (!BookOnShelf)
            hintText = "It is important to put your books back where they belong when you are done using them.";
        else if (!KeyObtained)
            hintText = "Have you checked for any changes at the desk?";

        return hintText;
    }
}
