using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlinkoPuzzle : Puzzle
{
    public PlinkoBall RedBall;
    public PlinkoBall YellowBall;
    public PlinkoBall GreenBall;
    public PlinkoBall BlueBall;
    public KeypadLock KeypadLock;
    public Plinko Plinko;
    public PlayerInventory Inventory;

    private bool LockStateInitialized;

    private bool RedBallObtained { get => Inventory.ContainsItem(RedBall) || Plinko.PlayedBalls.Contains(RedBall); }
    private bool YellowBallObtained { get => Inventory.ContainsItem(YellowBall) || Plinko.PlayedBalls.Contains(YellowBall); }
    private bool GreenBallObtained { get => Inventory.ContainsItem(GreenBall) || Plinko.PlayedBalls.Contains(GreenBall); }
    private bool BlueBallObtained { get => Inventory.ContainsItem(BlueBall) || Plinko.PlayedBalls.Contains(BlueBall); }

    // Start is called before the first frame update
    void Start()
    {
        // Puzzle is finished when Keypad lock is unlocked
        LockStateInitialized = false;
        KeypadLockListener();
    }

    // Update is called once per frame
    void Update()
    {
        if(!LockStateInitialized)
        {
            KeypadLockListener();
        }
    }

    public override string GetCurrentHint()
    {
        string hintText = "";

        if (!RedBallObtained)
            hintText = "Have a ball checking out the stuff on your coworker's desk.";
        else if (!YellowBallObtained)
            hintText = "Have a ball getting a little bit of light reading done.";
        else if (!GreenBallObtained)
            hintText = "Have a ball breaking down those old unused boxes.";
        else if (!BlueBallObtained)
            hintText = "Remember when you used to have a ball stacking chairs at the end of the day in Elementary School?";
        else if (RedBallObtained && YellowBallObtained && GreenBallObtained && BlueBallObtained && Plinko.PlayedBalls.Count != 4)
            hintText = "Maybe I can use all these balls with that machine in the back.";
        else if (Plinko.PlayedBalls.Count == 4)
            hintText = "I think the colors of those balls match the colors I saw over on the door's keypad.";

        return hintText;
    }

    private void KeypadLockListener()
    {
        if(KeypadLock.LockStateChange != null)
        {
            KeypadLock.LockStateChange.AddListener(() => { Completed = !KeypadLock.Locked; });
            LockStateInitialized = true;
        }
    }
}
