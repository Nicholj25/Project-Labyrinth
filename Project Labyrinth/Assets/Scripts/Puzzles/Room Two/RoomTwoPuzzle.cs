using UnityEngine;

public class RoomTwoPuzzle : Puzzle
{
    [SerializeField] private PuzzleInput riddle;
    [SerializeField] private ZoomItem monitor;
    [SerializeField] private ReappearItemAction donut;
    [SerializeField] private ReappearItemAction book;
    [SerializeField] private RigidbodiesHandler bamboo;
    [SerializeField] private GameObject smallBamboo;
    [SerializeField] private PlayerInventory inventory;
    public TextPrompt Text;
    public InventoryItem Key;
    [SerializeField] private bool hasZoomed;
    [SerializeField] private bool riddleSolved;
    [SerializeField] private bool inTrash;
    [SerializeField] private bool onTable;
    void Start()
    {
        Key.PickupCompletion.AddListener(() => { KeyObtained = true; Completed = true; });
        riddle.InteractionComplete.AddListener(() => { riddleSolved = true; });
        monitor.InteractionComplete.AddListener(() => { hasZoomed = true; });
        donut.InteractionComplete.AddListener(() => { if (!inTrash) TipBamboo(-45); inTrash = true; CheckBamboo(); });
        book.InteractionComplete.AddListener(() => { if (!onTable) TipBamboo(-45); onTable = true; CheckBamboo(); });

        // Start with key unobtained, Monitor not Zoomed, Riddle not Solved
        KeyObtained = false;
        hasZoomed = false;
        riddleSolved = false;

        Key.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (onTable && !book.gameObject.activeSelf)
        {
            onTable = false;
            TipBamboo(45);
        }
        else if (inTrash && !donut.gameObject.activeSelf)
        {
            inTrash = false;
            TipBamboo(45);
        }
        if (!onTable || !inTrash)
            bamboo.toggleRigidbody(true);

    }

    public override string GetCurrentHint()
    {
        string hintText = "";

        if (!hasZoomed)
            hintText = "Looks like the boss left the computer on too.";
        else if (hasZoomed && !riddleSolved)
            hintText = "Take a look around. I am in this room.";
        else if (riddleSolved)
            hintText = "Where did you see me last? Look there.";
        return hintText;
    }

    private void CheckBamboo()
    {
        if (inTrash && onTable)
        {
            bamboo.toggleRigidbody(false);
            Key.enabled = true;
        }
    }

    private void TipBamboo(float zAxis)
    {   
        smallBamboo.transform.eulerAngles = smallBamboo.transform.eulerAngles + new Vector3(0, 0, zAxis);
    }

}
