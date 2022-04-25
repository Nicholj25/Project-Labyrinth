 using UnityEngine;
 using System.Collections;
 
 [RequireComponent(typeof(Rigidbody))] //Make sure a rigidbody is attached to the falling object
 public class ProjectorOn : MonoBehaviour {
 
     private Shader defaultShader;
     private Shader transparentShader;
     Renderer rend;
     private Rigidbody rigidbody;

     void Start () {
         rend = GetComponent<Renderer>();
         defaultShader = Shader.Find("UI/DefaultETC1");
         transparentShader = Shader.Find("UI/Lit/Refraction");
         rigidbody = GetComponent<Rigidbody> ();
     }
 
    void OnMouseDown()
    {
        if (rend.material == transparentShader)
            {
                rend.material.shader = defaultShader;
            }
            if (rend.material == defaultShader)
            {
                rend.material.shader = transparentShader;
            }
    }

     void Update(){
         if (Input.GetMouseButtonDown(0)) {
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
