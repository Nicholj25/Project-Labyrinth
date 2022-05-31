using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [Header("Timer Controller")]

    /// <summary>
    /// Start time of the clock
    /// </summary>
    [SerializeField]
    private float StartTime;

    /// <summary>
    /// Time left in order to achieve the gold trophy
    /// </summary>
    [SerializeField]
    private float GoldTimeLeft;

    /// <summary>
    /// Time left in order to achieve the gold trophy
    /// </summary>
    [SerializeField]
    private float SilverTimeLeft;

    /// <summary>
    /// Current time left on the clock
    /// </summary>
    public float CurrentTime { get; private set; }

    public bool Failed { get; private set; }
    private LoadingScreen LoadingScreen;


    public enum TrophyType
    {
        Gold,
        Silver,
        Bronze
    }

    public TrophyType CurrentType { get; private set; }

    private void Awake()
    {
        Failed = false;
        CurrentTime = StartTime;
        CheckTrophyType();
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        CheckTrophyType();
        CheckLoss();
        if(Failed == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LoadingScreen.LoadNextRoom();
                Destroy(this.gameObject);
            }
        }
    }

    public void SetCurrentTime(float time)
    {
        CurrentTime = time;
    }

    public void CheckLoss()
    {
        if(CurrentTime <= 0 && !Failed)
        {
            UIHandler handler = FindObjectOfType<UIHandler>();
            PlayerCam cam = FindObjectOfType<PlayerCam>();
            PlayerMovement movement = FindObjectOfType<PlayerMovement>();

            if (handler.isUIActive())
            {
                handler.SwapScreens();
            }

            TextPrompt text = FindObjectOfType<TextPrompt>();
            text.UpdateTextBox("Unfortunately, your time has run out and you will be stuck in the office forever. However, if you press the space bar, you will be returned to the main menu and given another chance to escape!");

            // Freeze Movement and UI
            cam.IsFrozen = true;
            movement.isFrozen = true;
            handler.toggleUI(true);

            GameObject background = GameObject.Find("Background");

            LoadingScreen = this.gameObject.AddComponent<LoadingScreen>();
            LoadingScreen.roomToLoad = "Main Menu";
            LoadingScreen.MainCamera = GameObject.Find("Main Camera");
            LoadingScreen.LoadingCamera = background.transform.Find("Loading Camera").gameObject;
            LoadingScreen.LoadingPanel = background.transform.Find("LoadingPanel").gameObject;
            LoadingScreen.LoadingBar = background.transform.Find("LoadingPanel").Find("Loading Bar").GetComponent<Slider>();

            // Change back to normal cursor for main menu
            Cursor.lockState = CursorLockMode.None;

            Failed = true;
        }
}

    private void CheckTrophyType()
    {
        if (CurrentTime >= GoldTimeLeft)
        {
            CurrentType = TrophyType.Gold;
        }
        else if (CurrentTime >= SilverTimeLeft)
        {
            CurrentType = TrophyType.Silver;
        }
        else
        {
            CurrentType = TrophyType.Bronze;
        }
    }
}
