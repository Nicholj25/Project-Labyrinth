using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BasicWindow : MonoBehaviour
{
    public string newTitle;
    public string newMessage;
    private TMP_Text title;
    private TMP_Text message;
    private GameObject image;
    private GameObject rewardMessage;


    // Start is called before the first frame update
    void Start()
    {
        message = GameObject.Find("Access Message").GetComponent<TMP_Text>();
        title = GameObject.Find("Access Title").GetComponent<TMP_Text>();
        image = GameObject.Find("Access Image");
        rewardMessage = GameObject.Find("Reward Message");
        rewardMessage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateWindow()
    {
        title.text = newTitle;
        image.SetActive(false);
        rewardMessage.SetActive(true);
        message.text = newMessage;
    }
}
