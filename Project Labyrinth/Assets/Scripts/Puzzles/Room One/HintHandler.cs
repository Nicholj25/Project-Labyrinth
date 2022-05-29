using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HintHandler : Puzzle
{
    public PowerHandler Power;
    public MoveCabinet FileCabinetKey;
    public KeyPadALT FinalKey;
    public TextPrompt Text;

    private bool PowerIsOn;
    private bool FileCabinetKeyFound;
    private bool FinalKeyObtained;

   
    // Start is called before the first frame update 
    void Start()
    {
        Power.InteractionComplete.AddListener(() => { PowerIsOn = true; });
        FileCabinetKey.InteractionComplete.AddListener(() => { FileCabinetKeyFound = true; });
        FinalKey.InteractionComplete.AddListener(() => { FinalKeyObtained = true; });

        // Start with no progress
        PowerIsOn = false;
        FileCabinetKeyFound = false;
        FinalKeyObtained = false;
    }

    private void Update()
    {
        
    }  

    public override string GetCurrentHint()
    {
        string hintText = "";

        if (!PowerIsOn)
            hintText = "The power is out. There must be fuses lying around somewhere.";
        else if (!FileCabinetKeyFound)
            hintText = "Sometimes opening cabinets wildly is helpful.";
        else if (!FinalKeyObtained)
            hintText = "The boss always likes things in order and on time.";

        return hintText;
    }

}
