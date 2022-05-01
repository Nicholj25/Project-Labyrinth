using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintTextRoomThree : HintText
{
    protected override void DetermineHint()
    {
        if (!RoomPuzzles[0].Completed)
            UpdateHint(RoomPuzzles[0].GetCurrentHint());
        else
            UpdateHint("You've unlocked the door. Hurry and escape the room!");
    }
}
