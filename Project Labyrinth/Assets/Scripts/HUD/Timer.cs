using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Timer")]

    [SerializeField]
    private bool PauseTimer;

    /// <summary>
    /// Controller to save time when moving between rooms
    /// </summary>
    private TimerController Controller;

    /// <summary>
    /// Text component to display the timer
    /// </summary>
    private Text TimerText;

    /// <summary>
    /// Current time left on the clock
    /// </summary>
    private float CurrentTime;

    // Start is called before the first frame update
    void Start()
    {
        TimerText = this.gameObject.GetComponent<Text>();

        //Find Timer Controller
        Controller = GameObject.FindObjectOfType<TimerController>();

        //Get starting time
        CurrentTime = Controller.CurrentTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseTimer)
        {
            // DeltaTime : https://docs.unity3d.com/ScriptReference/Time-deltaTime.html
            CurrentTime -= Time.deltaTime;
        }
        Controller.SetCurrentTime(CurrentTime);
        UpdateTimer(CurrentTime);
    }

    /// <summary>
    /// Display the current time on the timer
    /// </summary>
    /// <param name="time"></param>
    void UpdateTimer(float time)
    {
        if (time > 0f)
        {
            string minutes = Mathf.FloorToInt(time / 60).ToString();
            string seconds = Mathf.FloorToInt(time % 60) < 10 ? $"0{Mathf.FloorToInt(time % 60)}" : Mathf.FloorToInt(time % 60).ToString();
            TimerText.text = $"{minutes}:{seconds}";
        }
        else
        {
            TimerText.text = "0:00";
        }
    }

    public void SetPauseTimer()
    {
        PauseTimer = true;
    }
}
