using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private UIHandler uiHandler;
    public List<GameObject> LockObjects;
    public List<ILock> Locks;
    public PlayerMovement playerMovement;
    public CameraHandler cameraHandler;
    //LoadingScreen loadRoom;
    LoadingScreen loadingScreen;

    private bool LocksInitialized;

    public bool Locked { get; private set; }

    /// <summary>
    /// TextPrompt script to display info
    /// </summary>
    public TextPrompt Text;

    private void Awake()
    {
        loadingScreen = GetComponent<LoadingScreen>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get list of door locks
        Locks = new List<ILock>();
        foreach (GameObject obj in LockObjects)
        {
            Locks.Add(obj.GetComponent<ILock>());
        }
        LocksInitialized = false;

        // Set initial locked state
        CheckLocked();
    }

    // Update is called once per frame
    void Update()
    {
        // Add listeners to all the locks
        if (!LocksInitialized)
        {
            if(Locks.All(x => x.LockStateChange != null))
            {
                foreach (ILock currentLock in Locks)
                {
                    currentLock.LockStateChange.AddListener(CheckLocked);
                    LocksInitialized = true;
                }
            }
        }

        if (playerMovement.isNearby(this.gameObject) && Input.GetMouseButtonDown(0) && cameraHandler.IsMainCameraActive() && !uiHandler.isUIActive())
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
        string locksRemaining = Locks.Count > 1 ? $"Number of Locks Remaining: {Locks.Where(x => x.Locked).Count()}" : "";
        if (Locked)
            Text.UpdateTextBox($"The door is locked. {locksRemaining}");
        else
        {
            loadingScreen.enabled = true;
            loadingScreen.LoadNextRoom(); 
        }
    }
}
