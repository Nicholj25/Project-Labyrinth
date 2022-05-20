using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlinkoBall : InventoryItem
{
    [Header("Plinko Ball")]

    [SerializeField]
    protected Plinko Board;

    [SerializeField]
    public Color BallColor;

    public enum Color
    {
        Red,
        Yellow,
        Green,
        Blue
    }

    private void OnTriggerEnter(Collider collider)
    {
        Board.UpdateBallValue(this, collider.transform.parent.name);
    }
}
