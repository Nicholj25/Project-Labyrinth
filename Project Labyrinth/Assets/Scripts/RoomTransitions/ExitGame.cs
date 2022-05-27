using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    void OnMouseDown()
    {
        Application.Quit();
        Debug.Log("Quitting...");
        
    }

    void Update()
    {

       if (Input.GetMouseButtonDown(0)) {
           if (Camera.main){
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null) {
                    hit.collider.attachedRigidbody.AddForce(Vector2.up);
                }
            }
        }
    }
}

