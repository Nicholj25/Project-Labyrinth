using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTwoPuzzle : Puzzle
{
    [SerializeField] private PuzzleInput riddle;
    [SerializeField] private ZoomItem monitor;
    public TextPrompt Text;
    public InventoryItem Key;
    [SerializeField] private bool hasZoomed;
    [SerializeField] private bool riddleSolved;
    void Start()
    {
        Key.PickupCompletion.AddListener(() => { KeyObtained = true; Completed = true; });
        riddle.InteractionComplete.AddListener(() => { riddleSolved = true; });
        monitor.InteractionComplete.AddListener(() => { hasZoomed = true; });

        // Start with key unobtained, Monitor not Zoomed, Riddle not Solved
        KeyObtained = false;
        hasZoomed = false;
        riddleSolved = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override string GetCurrentHint()
    {
        string hintText = "";

        if (!hasZoomed)
            hintText = "Looks like the boss left the computer on too.";
        else if (hasZoomed && !riddleSolved)
            hintText = "Take a look around. I am in this room.";
        else if (riddleSolved)
            hintText = "Where did you see me last? Look there.";
        return hintText;
    }
}
