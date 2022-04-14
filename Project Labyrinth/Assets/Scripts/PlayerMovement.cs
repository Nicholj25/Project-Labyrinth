using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 moveValues;
    CharacterController characterController;
    public float MovementSpeed = 5f;
    public float RotationSpeed = 5f;
    public float Gravity = 9.8f;
 
    // Source: https://docs.unity3d.com/ScriptReference/Transform.InverseTransformDirection.html
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
 
    void Update()
    {
        // Player movement - forward, backward and rotate left, right
        // Sources: https://www.youtube.com/embed/e5g1nJcjz-M
        //          https://forum.unity.com/threads/character-controller-move-local-vs-global-problem.107863/
        float horizontal = Input.GetAxis("Horizontal") * RotationSpeed;
        // Rotate Left and Right
        transform.Rotate(0, horizontal * Time.deltaTime, 0);
        Vector3 vertical = Vector3.zero;
        vertical.z = Input.GetAxis("Vertical") * MovementSpeed;



        // Forward Backward
        // Changes Direction to Local Direction to Maintain Perspective
        moveValues = transform.TransformDirection(vertical);
        characterController.Move(moveValues * Time.deltaTime);

    }
}