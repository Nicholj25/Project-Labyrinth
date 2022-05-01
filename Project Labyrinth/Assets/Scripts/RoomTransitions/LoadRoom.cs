using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadRoom : MonoBehaviour
{
    [SerializeField] private string room;
    [SerializeField] private CameraHandler cameraHandler;
    Camera cam;
    private void Start()
    {
        enabled = false;
    }

    public void LoadNextRoom()
    {
        SceneManager.LoadScene(room);
    }

    void Update()
    {
        cam = cameraHandler.GetCurrentCamera();
        if (Input.GetMouseButtonDown(0) && Camera.main)
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                hit.collider.attachedRigidbody.AddForce(Vector2.up);
            }
        }
    }
}
