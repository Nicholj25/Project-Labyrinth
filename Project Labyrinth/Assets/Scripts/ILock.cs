using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ILock
{
    public bool Locked { get; }

    public UnityEvent LockStateChange { get; }

}
