using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushItemAction : ItemInteraction
{
    private Rigidbody rb;
    public Vector3 transformDirection;
    public float thrust;
    public PlayerMovement player;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("Player Capsule").GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        
    }

    private void OnMouseDown()
    {
        if (player.isNearby(this.gameObject))
        {
            rb.AddForce(transformDirection * thrust, ForceMode.Impulse);
            InteractionComplete?.Invoke();
        }
        
    }


}
