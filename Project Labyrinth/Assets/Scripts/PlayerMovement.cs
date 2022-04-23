using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 moveValues;
    CharacterController characterController;
    public float MovementSpeed = 5f;
    public bool isFrozen;
    public float RotationSpeed = 5f;
    public float Gravity = 9.8f;
 
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        zMovement = Vector3.zero;
        rotationMovement = Vector3.zero;
        isFrozen = false;

    }
 
    void Update()
    {

        horizontal = Input.GetAxis("Horizontal") * RotationSpeed;
        zMovement.z = Input.GetAxis("Vertical") * MovementSpeed;
    }
    void FixedUpdate()
    {
        if (!isFrozen)
        {
            // Rotate Left and Right
            rotationMovement.y = horizontal;
            Quaternion deltaRotation = Quaternion.Euler(rotationMovement * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);

            // Forward Backward
            // Changes Direction to Local Direction to Maintain Perspective
            moveValues = transform.TransformDirection(zMovement);
            rb.MovePosition(transform.position + moveValues * Time.deltaTime);
        }
    }


    // Source: https://www.youtube.com/watch?v=qO06RnRmnSw
    public bool isNearby(GameObject clickableObject)
    {
        // Check Distance to Player
        float playerDistance = Vector3.Distance(clickableObject.transform.position, this.gameObject.transform.position);
        if (playerDistance <= 5)
        {
            return true;
        }

        return false;
    }
}