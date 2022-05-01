using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player movement - forward, backward and rotate left, right
// Sources: https://www.youtube.com/embed/e5g1nJcjz-M
//          https://forum.unity.com/threads/character-controller-move-local-vs-global-problem.107863/
//          https://stackoverflow.com/questions/39797696/gameobject-prefabs-floating-away
//          https://www.youtube.com/watch?v=ixM2W2tPn6c
public class PlayerMovement : MonoBehaviour
{
    Vector3 zMovement;
    Vector3 rotationMovement;
    Vector3 moveValues;
    public float MovementSpeed = 5f;
    public bool isFrozen;
    public float RotationSpeed = 5f;
    private float horizontal;
    Rigidbody rb;

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

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
    }
}