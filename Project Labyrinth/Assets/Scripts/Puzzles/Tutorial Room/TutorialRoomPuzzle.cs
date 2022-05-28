using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRoomPuzzle : Puzzle
{
    public BookOnShelfAction Trigger;
    public ZoomItem ZoomTrigger;
    public OpenableDrawer LockedDrawer;
    public ReappearItemAction ReappearingItem;
    public TextPrompt Text;
    public InventoryItem Key;

    private bool BookOnShelf;
    private bool KeyboardOnDesk;
   
    // Start is called before the first frame update 
    void Start()
    {
        ReappearingItem.ItemEquipped.AddListener(GiveReappearObjInstruction);
        ReappearingItem.InteractionComplete.AddListener(() => { KeyboardOnDesk = true; UnlockDrawer(); });
        ReappearingItem.ItemRemoved.AddListener(() => { KeyboardOnDesk = false; UnlockDrawer(); });
        ZoomTrigger.InteractionComplete.AddListener(GiveZoomOutInstruction);
        Trigger.InteractionComplete.AddListener(() => { BookOnShelf = true; UnlockDrawer();} );
        Key.PickupCompletion.AddListener(() => { KeyObtained = true; Completed = true; });

        // Start with key unobtained and book not on shelf and keyboad not on desk
        KeyboardOnDesk = false;
        BookOnShelf = false;
        KeyObtained = false;
    }

    private void Update()
    {
        if (!ReappearingItem.gameObject.activeSelf)
        {
            if(LockedDrawer.Opened)
                LockedDrawer.CloseDrawer();
            KeyboardOnDesk = false;
            UnlockDrawer();
        }
    }  

    private void GiveZoomOutInstruction()
    {
        Text.UpdateTextBox("Right click to zoom out.");
    }
    private void GiveReappearObjInstruction()
    {
        Text.UpdateTextBox("Right click to let go of the object.");
    }

    private void UnlockDrawer()
    {
        if (KeyboardOnDesk && BookOnShelf)
        {
            LockedDrawer.Locked = false;
            Text.UpdateTextBox("I think I heard a clicking sound coming from the desk.");
        }
        else
            LockedDrawer.Locked = true;
    }


    public override string GetCurrentHint()
    {
        string hintText = "";

        if (!KeyboardOnDesk)
            hintText = "I don't think that's where the keyboard belongs.";
        else if (!BookOnShelf)
            hintText = "It is important to put books back where they belong when they are not being used.";
        else if (!KeyObtained)
            hintText = "Maybe I should check for changes at the desk.";

        return hintText;
    }
}
