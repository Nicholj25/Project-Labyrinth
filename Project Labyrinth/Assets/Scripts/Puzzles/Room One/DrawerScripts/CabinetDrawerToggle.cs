using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetDrawerToggle : MonoBehaviour
{
    public bool Opened;
    public Transform player; //This creates a slot in the inspector where you can add your player
    private Rigidbody rigidBody;
    Animator openAnim;
    Animator closeAnim;

    void Start () {
        rigidBody = GetComponent<Rigidbody> ();
        //openAnim = gameObject.GetComponent<Animator>();
        // add closeAnim here
        Opened = false;
     }

    void OnMouseDown()
    {
        if (Opened == false) {
                gameObject.transform.position += new Vector3(0.3f, 0f, 0f);
                // Add open animation here instead
                Opened = true;
            }
            else {
                // Add close animation here instead
                gameObject.transform.position += new Vector3(-0.3f, 0f, 0f);
                Opened = false;
            }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Camera.main) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null) {
                Debug.Log(hit.collider.gameObject.name);
                hit.collider.attachedRigidbody.AddForce(Vector2.up);
            }
        }
    }
}
