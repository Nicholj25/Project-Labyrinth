using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodiesHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Rigidbody>() != null)
            {
                child.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleRigidbody(bool isKinem)
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Rigidbody>() != null)
            {
                Rigidbody rb = child.GetComponent<Rigidbody>(); 
                rb.isKinematic = isKinem;

            }
        }
    
    }

}
