using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeypadLock : MonoBehaviour, ILock
{
    [Header("Keypad Lock")]

    [SerializeField]
    protected Keypad Keypad;

    public bool Locked { get; private set; }

    public UnityEvent LockStateChange { get; set; }

    public bool KeypadEventSuccess { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Locked = true;
        LockStateChange = new UnityEvent();
        KeypadEventSuccess = false;
    }

    // Update is called once per frame
    protected void Update()
    {
        if(!KeypadEventSuccess && Keypad.SuccessfulEntry != null)
        {
            Keypad.SuccessfulEntry.AddListener(UnlockLock);
            KeypadEventSuccess = true;
        }
    }

    protected void UnlockLock()
    {
        Locked = false;
        LockStateChange.Invoke();
    }
}
