using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SlidePuzzle : ZoomItem
{
    //[Header("Slide Puzzle")]

    private List<GameObject> SlotsGO;
    private List<SlidePuzzleSlot> Slots;
    private SlidePuzzleSlot DisabledSlot;
    private GameObject PrizeSlot;
    private bool FirstRandomize;

    public bool PuzzleSolved { get; private set; }

    private Collider PuzzleCollider;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // Setup Colliders
        PuzzleCollider = this.gameObject.transform.GetComponent<Collider>();

        // Get Puzzle Drawer
        PrizeSlot = this.gameObject.transform.Find("Slide Out").gameObject;

        PuzzleSolved = false;

        Slots = this.gameObject.transform.Find("Slots").GetComponentsInChildren<SlidePuzzleSlot>().ToList();
        SlotsGO = new List<GameObject>();
        foreach (SlidePuzzleSlot slot in Slots)
        {
            SlotsGO.Add(slot.gameObject);
        }
        FindDisabled();
        FirstRandomize = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(FirstRandomize)
        {
            RandomizeImages();
            FirstRandomize = false;
        }
        if (Input.GetMouseButtonDown(0) && playerMovement.isNearby(this.gameObject))
        {
            cam = cameraHandler.GetCurrentCamera();
            if (cam != null)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);
                if (hit.transform.gameObject == this.gameObject && !inUse)
                    ActivateZoomCam();
                else if (SlotsGO.Contains(hit.transform.gameObject) && !PuzzleSolved)
                {
                    SlidePuzzleSlot clickedSlide = hit.transform.gameObject.GetComponent<SlidePuzzleSlot>();
                    if (DisabledSlot.AdjacentSlots.Contains(clickedSlide))
                    {
                        clickedSlide.SwapImages(DisabledSlot);
                        FindDisabled();
                        if (CheckCorrect())
                        {
                            PrizeSlot.GetComponentInChildren<Animator>().Play("Reveal Prize");
                            DisabledSlot.Disabled = false;
                            DisabledSlot.SetActive();
                            DisabledSlot = null;
                            PuzzleSolved = true;
                        }
                    }
                }
            }
        }
        else if (Input.GetMouseButton(1) && inUse)
        {
            ZoomOut();
        }
    }

    private bool CheckCorrect()
    {
        return Slots.All(x => x.StartingSprite == x.CurrentSprite);
    }

    private void FindDisabled()
    {
        DisabledSlot = Slots.Where(x => x.Disabled).FirstOrDefault();
    }

    public void RandomizeImages()
    {
        System.Random rnd = new System.Random();
        for (int i = 0; i < 100; i++)
        {
            int index = rnd.Next(DisabledSlot.AdjacentSlots.Count);
            DisabledSlot.AdjacentSlots[index].SwapImages(DisabledSlot);
            FindDisabled();
        }
    }

    /// <summary>
    /// Changes from Zoom Camera to Main Camera
    /// </summary>
    protected override void ZoomOut()
    {
        PuzzleCollider.enabled = true;
        base.ZoomOut();
    }

    /// <summary>
    /// Changes From Main Camera to Zoom Camera
    /// </summary>
    protected override void ActivateZoomCam()
    {
        PuzzleCollider.enabled = false;
        base.ActivateZoomCam();
    }
}
