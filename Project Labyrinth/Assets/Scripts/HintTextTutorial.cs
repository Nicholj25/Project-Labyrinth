using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintTextTutorial : HintText
{
    protected override void DetermineHint()
    {
        // Will need to be updated once there are puzzles in the room
        UpdateHint("All puzzles are completed in this room");
    }
}
