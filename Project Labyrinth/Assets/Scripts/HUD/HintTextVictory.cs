using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintTextVictory : HintText
{
    protected override void DetermineHint()
    {
        UpdateHint("You've made it through escape room! Claim your trophy!");
    }
}
