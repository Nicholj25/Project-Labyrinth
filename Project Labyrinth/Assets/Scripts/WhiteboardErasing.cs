using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WhiteboardErasing : ItemInteraction
{
    public Sprite DirtyBoard;
    public Sprite ErasedBoard;

    public bool Erased {get; private set;}
    private Image BoardContents;

    // Start is called before the first frame update
    void Start()
    {
        BoardContents = this.gameObject.GetComponentInChildren<Image>();
        BoardContents.sprite = DirtyBoard;
        Erased = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void ItemUsageAction()
    {
        // Erase the board if not erased
        if(!Erased)
        {
            Erased = true;
            BoardContents.sprite = ErasedBoard;

            Text.UpdateTextBox("Most of the marker wipes away revealing something underneath.");
            InteractionComplete.Invoke();
        }
    }
}
