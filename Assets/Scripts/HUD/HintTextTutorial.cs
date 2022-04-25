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
            UpdateHint("You've found the key. Hurry and escape the room!");
    }
}
