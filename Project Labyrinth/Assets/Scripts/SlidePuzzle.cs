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
        RandomizeImages();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cam = cameraHandler.GetCurrentCamera();
            if (cam != null)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);
                if (hit.transform.gameObject == this.gameObject && !inUse)
                    ActivateZoomCam();
                else if (SlotsGO.Contains(hit.transform.gameObject) && DisabledSlot)
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
        for (int i = 0; i < 100; i++)
        {
            int index = UnityEngine.Random.Range(0, DisabledSlot.AdjacentSlots.Count);
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
