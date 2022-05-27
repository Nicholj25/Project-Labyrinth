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

    public enum TrophyType
    {
        Gold,
        Silver,
        Bronze
    }

    public TrophyType CurrentType { get; private set; }

    private void Awake()
    {
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
    }

    public void SetCurrentTime(float time)
    {
        CurrentTime = time;
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
