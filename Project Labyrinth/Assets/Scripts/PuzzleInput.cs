using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleInput : ItemInteraction
{
    public GameObject puzzleInputWindow;
    [SerializeField] private GameObject closeButtonObject;
    [SerializeField] private GameObject submitButtonObject;
    [SerializeField] private GameObject startObject;
    [SerializeField] private GameObject successObject;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private string correctInput;
    private Button submitButton;
    private Button closeButton;
    private string inputString;

    void Awake()
    {
        correctInput = correctInput.ToLower();
        inputString = "";
        SetCloseButtonEvent();
        SetSubmitButtonEvent();
        Hide(this.gameObject);
        Hide(successObject);
        cam = Camera.main;
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    // Source: https://forum.unity.com/threads/solved-invoke-a-unity-button-click-event-from-c-script.722126/
    protected override void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return) && inputField.text.Length != 0)
            //Invoke the button's onClick event so enter also submits text
            submitButton.onClick.Invoke();

    }

    /// <summary>
    /// Displays GameObject
    /// </summary>
    /// <param name="obj"> GameObject to Activate</param>
    /// Source: https://www.youtube.com/watch?v=4n6RT805rCc
    public void Show(GameObject obj)
    {
        obj.SetActive(true);
    }

    /// <summary>
    /// Hides GameObject
    /// </summary>
    /// <param name="obj"> GameObject to Activate</param>
    /// Source: https://www.youtube.com/watch?v=4n6RT805rCc
    public void Hide(GameObject obj)
    {
        obj.SetActive(false);
    }

    /// <summary>
    /// Sets Up Close Button (X) on Input Window Onclick Event
    /// </summary>
    /// Source: https://docs.unity3d.com/2018.4/Documentation/ScriptReference/UI.InputField-onEndEdit.html
    public void SetCloseButtonEvent()
    {
        closeButton = closeButtonObject.GetComponent<Button>();
        closeButton.onClick.AddListener(delegate { Hide(this.gameObject); });
    }

    /// <summary>
    /// Sets Up Submit Button Onclick Event
    /// </summary>
    /// Source: https://docs.unity3d.com/2018.4/Documentation/ScriptReference/UI.InputField-onEndEdit.html
    public void SetSubmitButtonEvent()
    {
        submitButton = submitButtonObject.GetComponent<Button>();
        submitButton.onClick.AddListener(delegate { ReadStringInput(inputField.text); });
    }

    /// <summary>
    ///  Reads String From InputField
    /// </summary>
    /// <param name="input"></param>
    /// Source:  https://www.youtube.com/watch?v=guelZvubWFY
    private void ReadStringInput(string input)
    {
        inputString = input.ToLower();
        if (correctInput == inputString)
        {
            RunSuccess();
        }
        else
        {
            StartCoroutine(RunTryAgain());
        }

    }

    /// <summary>
    /// Changes an Object to Another Object
    /// successObject appears
    /// startObject disappears if it exists
    /// </summary>
    private void RunSuccess()
    {
        inputField.GetComponent<Image>().color = Color.green;

        // Add Positive Feedback Sound Here
        Debug.Log("Positive feedback sound here");

        // Success Object Appears and If Present, Start Object Disappears
        if (startObject)
        {
            Hide(startObject);
        }
        Show(successObject);

        InteractionComplete?.Invoke();

    }

    /// <summary>
    /// Changes Input Field Background to Red then back to white
    /// and Resets Field
    /// </summary>
    /// <returns>uses a co-routine to pause briefly</returns> 
    /// Source: https://stackoverflow.com/questions/30056471/how-to-make-the-script-wait-sleep-in-a-simple-way-in-unity
    IEnumerator RunTryAgain()
    {
        // Change Color of Background
        inputField.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(.5f);
        inputField.GetComponent<Image>().color = Color.white;

        // Add Negative Feedback Sound Here
        Debug.Log("negative feedback sound here");
        inputField.text = "";

    }

}
