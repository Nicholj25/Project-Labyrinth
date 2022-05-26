using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Keypad : ZoomItem
{
    [Header("Keypad")]

    [SerializeField]
    protected string ExpectedValue;

    public string CurrentValue { get; private set; }
    public List<GameObject> Numbers { get; private set; }
    public GameObject Clear { get; private set; }
    public GameObject Confirm { get; private set; }
    public Transform Digits { get; private set; }
    public bool CorrectValueFound { get; private set; }
    public List<Collider> ButtonColliders { get; private set; }
    public Collider KeypadCollider { get; private set; }
    public UnityEvent SuccessfulEntry { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        // Success Handler
        SuccessfulEntry = new UnityEvent();
    }
    // Start is called before the first frame update
    protected void Start()
    {

        // Default value
        CurrentValue = "";

        // Get Keypad collider
        KeypadCollider = this.gameObject.GetComponent<Collider>();

        // Setup number buttons
        Transform numParent = this.gameObject.transform.Find("Numbers");
        Numbers = new List<GameObject>();
        ButtonColliders = new List<Collider>();
        foreach (Transform num in numParent.transform)
        {
            if(num.parent == numParent)
            {
                Numbers.Add(num.gameObject);
                ButtonColliders.Add(num.GetComponent<Collider>());
            }
        }

        // Setup Clear button
        Clear = this.gameObject.transform.Find("Clear").gameObject;
        ButtonColliders.Add(Clear.GetComponent<Collider>());
        
        // Setup Confirm button
        Confirm = this.gameObject.transform.Find("Confirm").gameObject;
        ButtonColliders.Add(Confirm.GetComponent<Collider>());

        // Set initial collider status
        EnableButtonColliders(false);

        // Setup digits for displaying entered code
        Digits = this.gameObject.transform.Find("View").Find("Digits");

        DisplayCode();
    }

    /// <summary>
    /// Switches the enabled status of the keypad and the buttons
    /// </summary>
    /// <param name="enabled">Buttons are enabled</param>
    private void EnableButtonColliders(bool enabled)
    {
        KeypadCollider.enabled = !enabled;
        foreach (Collider col in ButtonColliders)
        {
            col.enabled = enabled;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cam = cameraHandler.GetCurrentCamera();
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (hit.transform.gameObject == this.gameObject && !inUse)
                ActivateZoomCam();
            else if (Numbers.Contains(hit.transform.gameObject))
                NumberButton(hit.transform.name);
            else if (hit.transform.gameObject == Clear)
                ClearButton();
            else if (hit.transform.gameObject == Confirm)
                ConfirmButton();
        }
        else if (Input.GetMouseButton(1) && inUse)
        {
            ZoomOut();
        }
    }

    protected void NumberButton(string num)
    {
        if (CurrentValue.Length < 4)
        {
            CurrentValue += num;
            DisplayCode();
        }
    }

    protected void ConfirmButton()
    {
        CorrectValueFound = CurrentValue.Equals(ExpectedValue);
        float red = CurrentValue.Equals(ExpectedValue) ? 0 : 255;
        float green = CurrentValue.Equals(ExpectedValue) ? 255 : 0;
        Color newColor = new Color(red, green, 0);
        foreach (Transform digit in Digits)
        {
            digit.GetComponent<TextMeshProUGUI>().color = newColor;
        }

        if (CorrectValueFound)
            SuccessfulEntry.Invoke();
    }

    protected void ClearButton()
    {
        CurrentValue = "";
        DisplayCode();
    }

    protected void DisplayCode()
    {
        int startingIndex = 4 - CurrentValue.Length;
        foreach (Transform digit in Digits)
        {
            int index = digit.GetSiblingIndex();
            TextMeshProUGUI textMesh = digit.GetComponent<TextMeshProUGUI>();
            if (index < startingIndex)
            {
                textMesh.text = "";
                textMesh.color = new Color(255, 255, 255);
            }
            else
            {
                textMesh.text = CurrentValue[index - startingIndex].ToString();
                textMesh.color = new Color(255, 255, 255);
            }
        }
    }

    /// <summary>
    /// Changes from Zoom Camera to Main Camera
    /// </summary>
    protected override void ZoomOut()
    {
        EnableButtonColliders(false);
        base.ZoomOut();
    }

    /// <summary>
    /// Changes From Main Camera to Zoom Camera
    /// </summary>
    protected override void ActivateZoomCam()
    {
        EnableButtonColliders(true);
        base.ActivateZoomCam();
    }

    public void SetExpectedValue(string value)
    {
        ExpectedValue = value;
    }
}
