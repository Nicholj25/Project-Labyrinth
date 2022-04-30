using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System;

public class ComputerLockLogin : MonoBehaviour, ILock
{
    [SerializeField] private string Username;
    [SerializeField] private string Password;
    [SerializeField] private GameObject LoginScreen;
    [SerializeField] private GameObject UnlockScreen;
    [SerializeField] private TMP_InputField UsernameInput;
    [SerializeField] private TMP_InputField PasswordInput;
    [SerializeField] private Button LoginButton;
    [SerializeField] private GameObject InvalidLogin;
    [SerializeField] private Button LockButton;
    [SerializeField] private Button UnlockButton;

    [SerializeField] private bool RequireLogin;

    private string UsernameString;
    private string PasswordString;

    public bool Locked { get; private set; }

    public UnityEvent LockStateChange { get; set; }

    private void Awake()
    {
        Locked = true;

        UsernameString = "";
        PasswordString = "";

        InvalidLogin.SetActive(false);
        LoginButton.onClick.AddListener(delegate { VerifyLogin(); });
        UnlockButton.onClick.AddListener(delegate { UpdateLockStatus(); });
        LockButton.onClick.AddListener(delegate { UpdateLockStatus(); });

        if(RequireLogin)
        {
            LoginScreen.SetActive(true);
            UnlockScreen.SetActive(false);
        }
        else
        {
            LoginScreen.SetActive(false);
            UnlockScreen.SetActive(true);
        }

        UnlockButton.gameObject.SetActive(Locked);
        LockButton.gameObject.SetActive(!Locked);
    }

    /// <summary>
    /// Updates the locked state
    /// </summary>
    private void UpdateLockStatus()
    {
        // Change locked status
        Locked = !Locked;

        // Display correct button
        UnlockButton.gameObject.SetActive(Locked);
        LockButton.gameObject.SetActive(!Locked);

        LockStateChange.Invoke();
    }

    // Start is called before the first frame update
    private void Start()
    {
        LockStateChange = new UnityEvent();
    }

    void Update()
    {
        if(PasswordInput.text.Length != PasswordString.Length)
        {
            DisplayPassword();
        }
        
        if (Input.GetKeyDown(KeyCode.Return) && LoginScreen.activeSelf && (UsernameInput.text.Length != 0 || PasswordInput.text.Length != 0))
        {
            //Invoke the button's onClick event.
            LoginButton.onClick.Invoke();
        }
    }

    /// <summary>
    /// Saves password to a string and converts display text to asterisks
    /// </summary>
    private void DisplayPassword()
    {
        //Check if backspace was used
        if (PasswordString.Length > PasswordInput.text.Length)
        {
            PasswordString = PasswordString.Remove(PasswordString.Length - 1);
        }
        else
        {
            PasswordString += PasswordInput.text[PasswordInput.text.Length - 1];
        }

        string tempPasswordText = "";
        for (int i = 0; i < PasswordInput.text.Length; i++)
        {
            tempPasswordText += "*";
        }
        PasswordInput.text = tempPasswordText;
    }

    /// <summary>
    /// Check if login attempt is valid
    /// </summary>
    private void VerifyLogin()
    {
        UsernameString = UsernameInput.text.ToLower();
        if(UsernameString.Equals(Username.ToLower()) && PasswordString.Equals(Password))
        {
            LoginScreen.SetActive(false);
            UnlockScreen.SetActive(true);
        }
        else 
        {
            InvalidLogin.SetActive(true);
        }
    }
}
