using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissingUserPuzzle : Puzzle
{
    public WhiteboardErasing Erasing;
    public InventoryItem StickyNote;
    public InventoryItem WhiteboardEraser;
    public InventoryItem Paper;
    public ComputerLockLogin ComputerLock;
    public PlayerInventory Inventory;

    private bool StickyNoteObtained { get => Inventory.ContainsItem(StickyNote); }
    private bool WhiteboardEraserObtained { get => Inventory.ContainsItem(WhiteboardEraser); }
    private bool PaperObtained { get => Inventory.ContainsItem(Paper); }
    private bool WhiteboardErased { get => Erasing.Erased; }

    // Start is called before the first frame update
    void Start()
    {
        // Puzzle is finished when Computer lock is unlocked
        ComputerLock.LockStateChange.AddListener(() => { Completed = !ComputerLock.Locked; });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override string GetCurrentHint()
    {
        string hintText = "";

        if (!WhiteboardEraserObtained)
            hintText = "I think I might have seen something helpful on the stacked L-shaped desks in the back.";
        else if (WhiteboardEraserObtained && !WhiteboardErased)
            hintText = "I found an eraser, so I better get to erasing!";
        else if (!PaperObtained)
            hintText = "I wonder if anybody threw anything helpful away?";
        else if (!StickyNoteObtained)
            hintText = "Is there something sticking to that metal desk in the back?";
        else if (StickyNoteObtained && PaperObtained && WhiteboardErased)
            hintText = "I think I might have all the info I need to log into the computer";

        return hintText;
    }
}
