using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDoorInteraction : MonoBehaviour
{
    [SerializeField] private UIHandler uiHandler;
    public List<GameObject> LockObjects;
    public List<ILock> Locks;
    public PlayerMovement playerMovement;
    public CameraHandler cameraHandler;
    Animator doorAnim;

    public bool Locked { get; private set; }

    /// <summary>
    /// TextPrompt script to display info
    /// </summary>
    public TextPrompt Text;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        doorAnim = gameObject.GetComponent<Animator>();
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
        if (Camera.main)
        {
            if(playerMovement.isNearby(this.gameObject) && Input.GetMouseButtonDown(0) && cameraHandler.IsMainCameraActive() && !uiHandler.isUIActive())
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
    }

    public void CheckLocked()
    {
        Locked = Locks.Any(x => x.Locked);
    }

    protected void OpenDoor()
    {
        if (Locked)
            Text.UpdateTextBox("The door is locked.");
        else
        {
            // causes door opening animation to play
            doorAnim.enabled = true;
            doorAnim.Play("DoorOpen");
            // Turns off door collider so player can walk through
            GetComponent<Collider>().enabled = false;
            foreach(Collider m_collider in GetComponentsInChildren<Collider>())
            {
                m_collider.enabled = false;
            }
            
        }
    }
}
