using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HintTextRoomThree : HintText
{
    protected override void DetermineHint()
    {
        Puzzle nextPuzzle = RoomPuzzles.Where(x => !x.Completed).FirstOrDefault();
        if (nextPuzzle != null)
            UpdateHint(nextPuzzle.GetCurrentHint());
        else
            UpdateHint("You've unlocked the door. Hurry and escape the room!");
    }
}