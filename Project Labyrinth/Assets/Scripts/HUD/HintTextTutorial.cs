using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintTextTutorial : HintText
{
    protected override void DetermineHint()
    {
        if (!RoomPuzzles[0].Completed)
            UpdateHint(RoomPuzzles[0].GetCurrentHint());
        else
            UpdateHint("I found the key. Let me try equipping it and testing the door handle. If it unlocks, I'll try the door.");
    }
}
