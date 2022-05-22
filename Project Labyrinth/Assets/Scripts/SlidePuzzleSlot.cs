using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SlidePuzzleSlot : MonoBehaviour
{
    public bool Disabled;
    public List<SlidePuzzleSlot> AdjacentSlots;

    public Collider Collider { get; private set; }

    public Image ImageObject { get; private set; }

    public Sprite CurrentSprite { 
        get
        {
            return ImageObject.sprite;
        }
    }

    public Sprite StartingSprite { get; private set; }

    // Start is called before the first frame update
    protected void Start()
    {
        Collider = this.gameObject.GetComponent<Collider>();
        ImageObject = this.gameObject.transform.GetComponentInChildren<Image>();
        StartingSprite = ImageObject.sprite;
        SetActive();
    }

    // Update is called once per frame
    protected void Update()
    {
    }

    public void SetActive()
    {
        ImageObject.gameObject.SetActive(!Disabled);
    }

    public void SwapImages(SlidePuzzleSlot switcher)
    {
        Sprite temp = CurrentSprite;
        ImageObject.sprite = switcher.CurrentSprite;
        switcher.ImageObject.sprite = temp;

        // Swap disabled images
        switcher.Disabled = false;
        switcher.SetActive();
        Disabled = true;
        SetActive();
    }
}
