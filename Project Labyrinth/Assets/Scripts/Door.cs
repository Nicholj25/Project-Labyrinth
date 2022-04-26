using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public List<GameObject> LockObjects;
    public List<ILock> Locks;
    public PlayerMovement playerMovement;
    public ZoomItem zoomItem;

    public bool Locked { get; private set; }

    /// <summary>
    /// TextPrompt script to display info
    /// </summary>
    public TextPrompt Text;

    // Start is called before the first frame update
    void Start()
    {
        // Get list of door locks
        Locks = new List<ILock>();
        foreach (GameObject obj in LockObjects)
        {
            Locks.Add(obj.GetComponent<ILock>());
        }

        // Add listeners to all the locks
        foreach (ILock currentLock in Locks)
        {
            currentLock.LockStateChange.AddListener(CheckLocked);
        }

        // Set initial locked state
        CheckLocked();
    }

    // Update is called once per frame
    void Update()
    {
        bool isMainCameraActive = false;
        if (zoomItem.getCurrentCamera())
        {
            isMainCameraActive = zoomItem.getCurrentCamera() == Camera.main;
        }
        if(playerMovement.isNearby(this.gameObject) && Input.GetMouseButtonDown(0) && isMainCameraActive)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (hit.transform.gameObject == this.gameObject)
            {
                OpenDoor();
            }
        }
    }

    public void CheckLocked()
    {
        Locked = Locks.Any(x => x.Locked);
    }

    protected void OpenDoor()
    {
        if (Locked)
            Text.UpdateTextBox("The door is locked.");
        
        // ToDo: Implement door opening functionalities
    }
}
