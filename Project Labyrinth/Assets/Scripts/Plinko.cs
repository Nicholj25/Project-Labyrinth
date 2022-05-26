using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plinko : ItemInteraction
{
    [Header("Plinko")]

    [SerializeField]
    protected Keypad Keypad;

    private float XPosition;
    private float YPosition = 25f;
    private float ZPosition = 2.5f;

    private Rigidbody CurrentBall;
    private Collider Collider;
    private bool BallMotionStarted;

    private string RedValue;
    private string BlueValue;
    private string GreenValue;
    private string YellowValue;

    public List<Collider> Spots { get; private set; }

    public List<PlinkoBall> PlayedBalls { get; private set; }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Collider = this.gameObject.GetComponent<Collider>();

        Spots = this.transform.Find("Spots").GetComponentsInChildren<Collider>().ToList();

        PlayedBalls = new List<PlinkoBall>();
    }

    protected override void Update()
    {
        cam = cameraHandler.GetCurrentCamera();
        if (playerMovement.isNearby(this.gameObject) && Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (hit.transform.gameObject == this.gameObject && AcceptableItems.Contains(Inventory.CurrentItem))
            {
                // Stop collider from messing with physics
                Collider.enabled = false;

                // Starting ball motion
                BallMotionStarted = true;

                // Put ball in good position
                XPosition = transform.InverseTransformPoint(hit.point).x;
                XPosition = XPosition % 1.5 == 0 ? XPosition + .1f : XPosition;

                ItemUsageAction();
            }
        }

        if(CurrentBall != null)
        {
            float speed = CurrentBall.velocity.magnitude;
            if (speed == 0 && !BallMotionStarted)
            {
                Collider.enabled = true;
                CurrentBall.isKinematic = true;
            }
            else if (speed > 0)
            {
                BallMotionStarted = false;
            }
        }
    }

    protected override void ItemUsageAction()
    {
        // Add to ball list
        CurrentBall = Inventory.CurrentItem.gameObject.GetComponent<Rigidbody>();

        // Moving ball to correct location
        Inventory.CurrentItem.transform.parent = this.transform;
        Inventory.CurrentItem.gameObject.SetActive(true);
        Inventory.CurrentItem.gameObject.transform.localPosition = new Vector3(XPosition, YPosition, ZPosition);
        Inventory.CurrentItem.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

        // Set ball to active
        Rigidbody ball = Inventory.CurrentItem.GetComponent<Rigidbody>();
        ball.isKinematic = false;

        // Remove ball from inventory
        Inventory.RemoveItem(Inventory.CurrentItem);
        Inventory.UnequipItem();

        InteractionComplete?.Invoke();
    }

    public void UpdateBallValue(PlinkoBall ball, string number)
    {
        switch (ball.BallColor)
        {
            case PlinkoBall.Color.Red:
                RedValue = number;
                break;
            case PlinkoBall.Color.Yellow:
                YellowValue = number;
                break;
            case PlinkoBall.Color.Green:
                GreenValue = number;
                break;
            case PlinkoBall.Color.Blue:
                BlueValue = number;
                break;
            default:
                break;
        }

        if(!PlayedBalls.Contains(ball))
            PlayedBalls.Add(ball);

        if (RedValue != null && YellowValue != null && GreenValue != null && BlueValue != null)
        {
            Keypad.SetExpectedValue(RedValue + YellowValue + GreenValue + BlueValue);
        }
    }
}
