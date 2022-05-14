using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyLock : ItemInteraction, ILock
{

    public bool Locked { get; private set; }

    public UnityEvent LockStateChange { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        Locked = true;
        LockStateChange = new UnityEvent();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void ItemUsageAction()
    {
        // Check for correct key
        if (Item == Inventory.CurrentItem)
        {
            if (Locked)
                Text.UpdateTextBox("The key is inserted into the lock. It turns and unlocks.");
            else
                Text.UpdateTextBox("The key is inserted into the lock. It turns and locks.");
            Locked = !Locked;

            LockStateChange.Invoke();
        }

        // Check for key that fits but isn't correct
        else
            Text.UpdateTextBox("The key is inserted into the lock. However, it will not turn.");
    }
}
