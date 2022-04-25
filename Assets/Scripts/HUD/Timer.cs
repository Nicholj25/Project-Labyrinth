using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
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

        //Temporary time start will be updated when new scenes are added
        CurrentTime = 300f;
    }

    // Update is called once per frame
    void Update()
    {
        // DeltaTime : https://docs.unity3d.com/ScriptReference/Time-deltaTime.html
        CurrentTime -= Time.deltaTime;
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
}
