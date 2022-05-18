using UnityEngine;

public class DragObjectAction : ZoomItem
{
    [SerializeField] private float zPosition;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = this.gameObject.transform.position;
        cam = Camera.main;
    }

    private void OnMouseDrag()
    {
        cam = cameraHandler.GetCurrentCamera();

        if (cam == zoomCam.GetComponent<Camera>())
        {
            Vector3 mousePosition = Input.mousePosition;
            this.gameObject.transform.position = cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, zPosition));
        }
    }

    private void OnMouseUp()
    {
        this.gameObject.transform.position = startPosition;
    }

}
