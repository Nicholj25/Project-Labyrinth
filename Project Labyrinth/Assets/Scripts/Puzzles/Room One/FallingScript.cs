 using UnityEngine;
 using System.Collections;
 
 [RequireComponent(typeof(Rigidbody))] //Make sure a rigidbody is attached to the falling object
 public class FallingScript : MonoBehaviour {
 
     public Transform player; //This creates a slot in the inspector where you can add your player
     private Rigidbody rb;
     void Start () {
         rb = GetComponent<Rigidbody> ();
         rb.useGravity = false;
     }
 
    void OnMouseDown()
    {
        rb.useGravity = true;
    }

     void Update(){
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