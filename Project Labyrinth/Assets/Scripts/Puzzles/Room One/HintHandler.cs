using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Threading.Tasks;
using System;

public class HintHandler : Puzzle
{
    public PowerHandler Power;
    public MoveCabinet FileCabinetKey;
    public KeyPadALT FinalKey;
    public TextPrompt Text;

    private bool PowerIsOn;
    private bool FileCabinetKeyFound;
    private bool FinalKeyObtained;
    private bool PowerFlickeredOut;

   
    // Start is called before the first frame update 
    void Start()
    {
        Power.InteractionComplete.AddListener(() => { PowerIsOn = true; });
        FileCabinetKey.InteractionComplete.AddListener(() => { FileCabinetKeyFound = true; });
        FinalKey.InteractionComplete.AddListener(() => { FinalKeyObtained = true; });

        // Start with no progress
        PowerFlickeredOut = false;
        PowerIsOn = false;
        FileCabinetKeyFound = false;
        FinalKeyObtained = false;

    }

    async private void Update()
    {
        await WaitOneSecondAsync(21);
        PowerFlickeredOut = true;
    }  

    public override string GetCurrentHint()
    {
        string hintText = "";

        if (!PowerFlickeredOut)
            hintText = "The lights are buzzing. Is this an issue?";
        else if (!PowerIsOn)
            hintText = "The power is out. There must be fuses lying around somewhere.";
        else if (!FileCabinetKeyFound)
            hintText = "Sometimes opening cabinets wildly is helpful.";
        else if (!FinalKeyObtained)
            hintText = "The boss always likes things in order and on time.";

        return hintText;
    }

    // Add a short delay
    public async Task WaitOneSecondAsync(double seconds)
    {
        await Task.Delay(TimeSpan.FromSeconds(seconds));
    }

}
