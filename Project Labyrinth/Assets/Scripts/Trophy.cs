using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trophy : MonoBehaviour
{
    [Header("Trophy")]

    [SerializeField] private UIHandler uiHandler;
    public PlayerMovement playerMovement;
    public CameraHandler cameraHandler;
    
    LoadingScreen loadingScreen;

    /// <summary>
    /// Controller to save time when moving between rooms
    /// </summary>
    private TimerController Controller;


    /// <summary>
    /// TextPrompt script to display info
    /// </summary>
    public TextPrompt Text;

    private bool TextDisplayed = false;

    private void Awake()
    {
        loadingScreen = GetComponent<LoadingScreen>();

        //Find Timer Controller
        Controller = GameObject.FindObjectOfType<TimerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<CursorHoverEffect>();
        SetTrophyType();
    }

    // Update is called once per frame
    void Update()
    {
        if (!TextDisplayed && Text != null)
        {
            Text.UpdateTextBox($"Congratulations! You have reached the end of the office. Your time has awarded you the {Controller.CurrentType.ToString()} Trophy! Claiming your trophy will return you to the main menu.");
            TextDisplayed = true;
        }
        if (playerMovement.isNearby(this.gameObject) && Input.GetMouseButtonDown(0) && cameraHandler.IsMainCameraActive() && !uiHandler.isUIActive())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (hit.transform.gameObject == this.gameObject)
            {
                CompleteGame();
            }
        }
    }

    protected void CompleteGame()
    {
        // Destroy timer controller to be recreated at menu
        Destroy(Controller.gameObject);

        // Change back to normal cursor for main menu
        Cursor.lockState = CursorLockMode.None;

        loadingScreen.enabled = true;
        loadingScreen.LoadNextRoom(); 
    }

    private void SetTrophyType()
    {
        this.gameObject.transform.Find("Gold").gameObject.SetActive(Controller.CurrentType == TimerController.TrophyType.Gold);
        this.gameObject.transform.Find("Silver").gameObject.SetActive(Controller.CurrentType == TimerController.TrophyType.Silver);
        this.gameObject.transform.Find("Bronze").gameObject.SetActive(Controller.CurrentType == TimerController.TrophyType.Bronze);
    }
}
