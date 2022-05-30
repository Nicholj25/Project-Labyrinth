using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotivationalPuzzlePoster : Puzzle
{
    public SlidePuzzle SlidePuzzle;
    public InventoryItem Key;
    public KeyLock KeyLock;
    public PlayerInventory Inventory;

    public bool LockStateInitialized { get; private set; }

    private bool PosterKeyObtained { get => Inventory.ContainsItem(Key); }
    private bool PuzzleFinished { get => SlidePuzzle.PuzzleSolved; }

    // Start is called before the first frame update
    void Start()
    {
        // Puzzle is finished when Key lock is unlocked
        LockStateInitialized = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!LockStateInitialized)
        {
            KeypadLockListener();
        }
    }

    public override string GetCurrentHint()
    {
        string hintText = "";

        if (!PuzzleFinished)
            hintText = "That motivational poster doesn't look quite right.";
        else if (!PosterKeyObtained)
            hintText = "Can't forget to pick up that key.";
        else
            hintText = "Better use the key to unlock the door handle!";

        return hintText;
    }

    private void KeypadLockListener()
    {
        if (KeyLock.LockStateChange != null)
        {
            KeyLock.LockStateChange.AddListener(() => { Completed = !KeyLock.Locked; });
            LockStateInitialized = true;
        }
    }
}
