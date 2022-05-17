using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushItemAction : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 transformDirection;
    public float thrust;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    private void OnMouseDown()
    {
        rb.AddForce(transformDirection * thrust, ForceMode.Impulse);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
