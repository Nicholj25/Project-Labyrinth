using UnityEngine;

public class DragObjectAction : ZoomItem
{
    [SerializeField] private ReappearItemAction reappearItem;
    [SerializeField] private float zPosition;
    [SerializeField] private Vector3 minBorder;
    [SerializeField] private Vector3 maxBorder;
    [SerializeField] private Vector3 zoomInRotation;
    private Vector3 zoomOutRotation;
    private Vector3 startPosition;

    protected override void Start()
    {
        base.Start();
        GetComponent<ZoomItem>().zoomOutTrigger.AddListener(returnToStart);
        zoomOutRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        this.gameObject.transform.rotation = Quaternion.Euler(zoomOutRotation);
        startPosition = this.gameObject.transform.position;
        cam = Camera.main;
    }

    private void OnMouseDrag()
    {
        cam = cameraHandler.GetCurrentCamera();

        if (cam == zoomCam.GetComponent<Camera>())
        {
            this.gameObject.transform.rotation = Quaternion.Euler(zoomInRotation);
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, zPosition));
            mousePosition = reappearItem.GetValidPosition(mousePosition, minBorder, maxBorder);
            this.gameObject.transform.position = mousePosition;
        }

    }

    private void returnToStart()
    {
        this.gameObject.transform.position = startPosition;
        this.gameObject.transform.rotation = Quaternion.Euler(zoomOutRotation);
    }
}
