using UnityEngine;

public class RoomTwoPuzzle : Puzzle
{
    [SerializeField] private PuzzleInput riddle;
    [SerializeField] private ZoomItem monitor;
    [SerializeField] private ZoomItem station7;
    [SerializeField] private ZoomItem whiteboard;
    [SerializeField] private ZoomItem paperPile;
    [SerializeField] private ReappearItemAction donut;
    [SerializeField] private ReappearItemAction book;
    [SerializeField] private RigidbodiesHandler bamboo;
    [SerializeField] private PushItemAction secondBamboo;
    [SerializeField] private PushItemAction fourthBamboo;
    [SerializeField] private Keypad keypad;
    [SerializeField] private MicrowaveAction microwave;
    [SerializeField] private GameObject smallBamboo;
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private AudioSource station7Audio;
    [SerializeField] private AudioSource smallBambooAudio;
    [SerializeField] private AudioSource bamboo2Audio;

    public TextPrompt Text;
    public InventoryItem Key;
    private bool hasZoomed;
    private bool riddleSolved;
    private bool station7Zoomed;
    private bool whiteboardZoomed;
    private bool inTrash;
    private bool onTable;
    private bool codeZoomed;
    private bool microwaveUnlocked;
    private bool microwaveOpened;
    private bool bambooMoved;
    void Start()
    {
        Key.PickupCompletion.AddListener(
            () => { KeyObtained = true; Completed = true; });
        riddle.InteractionComplete.AddListener(
            () => { riddleSolved = true; station7Audio.Play();});
        monitor.InteractionComplete.AddListener(
            () => { hasZoomed = true; });
        station7.InteractionComplete.AddListener(
            () => { station7Zoomed = true; });
        whiteboard.InteractionComplete.AddListener(
            () => { whiteboardZoomed = true; });
        donut.InteractionComplete.AddListener(
            () => { if (!inTrash) TipBamboo(-45); inTrash = true; CheckBamboo(); });
        book.InteractionComplete.AddListener(
            () => { if (!onTable) TipBamboo(-45); onTable = true; CheckBamboo(); });
        donut.ItemRemoved.AddListener(
            () => { if (onTable) TipBamboo(45); onTable = false; CheckBamboo(); });
        book.ItemRemoved.AddListener(
            () => { if (onTable) TipBamboo(45); onTable = false; CheckBamboo(); });
        paperPile.InteractionComplete.AddListener(
            () => { codeZoomed = true; });
        keypad.SuccessfulEntry.AddListener(
            () => { microwaveUnlocked = true; });
        microwave.InteractionComplete.AddListener(
            () => { microwaveOpened = true; });
        secondBamboo.InteractionComplete.AddListener(
            () => { bambooMoved = true; });
        fourthBamboo.InteractionComplete.AddListener(
            () => { bambooMoved = true; });

        // Start with key unobtained, Monitor not Zoomed, Riddle not Solved
        KeyObtained = false;
        hasZoomed = false;
        riddleSolved = false;
        station7Zoomed = false;
        whiteboardZoomed = false;
        inTrash = false;
        onTable = false;
        codeZoomed = false;
        microwaveUnlocked = false;
        microwaveOpened = false;
        bambooMoved = false;
        Key.enabled = false;

    }

   // Update is called once per frame
    void Update()
    {
        if (onTable && !book.gameObject.activeSelf)
        {
            onTable = false;
            TipBamboo(45);
            if (inTrash)
                bamboo2Audio.Play();
        }
        else if (inTrash && !donut.gameObject.activeSelf)
        {
            inTrash = false;
            TipBamboo(45);
            if(onTable)
                bamboo2Audio.Play();
        }
        if (!onTable || !inTrash)
        {
            bamboo.toggleRigidbody(true);
        }

    }

    public override string GetCurrentHint()
    {
        string hintText = "";

        if (!hasZoomed)
            hintText = "Looks like the boss was working on something.";
        else if (!riddleSolved)
            hintText = "Hmm...the answer is probably an object in this room." +
                "  The boss isn't very creative with passwords.";
        else if (riddleSolved && !station7Zoomed)
            hintText = "Let me take a closer look at workstation 7.";
        else if (station7Zoomed && riddleSolved && !whiteboardZoomed)
            hintText = "Seems like my colleague at station 7 was working " +
                "with something on the whiteboard.";
        else if(!microwaveUnlocked && !codeZoomed)
            hintText = "My colleague at station 8 didn't want to forget an " +
                "important code.";
        else if(!microwaveUnlocked && codeZoomed)
            hintText = "I remember my colleague at station 8 didn't want to " +
                "forget an important code. Let me find a keypad to enter it.";
        else if(microwaveUnlocked && !microwaveOpened)
            hintText = "The microwave unlocked. I'll try opening it.";
        else if (station7Zoomed && microwaveOpened && whiteboardZoomed && !(inTrash && onTable))
            hintText = "The email at station 7 was Bee = Tea. B=T could be " +
                "the key to decode the whiteboard instructions. Bee sure to " +
                "follow them.";
        else if(microwaveOpened && !bambooMoved && inTrash && onTable)
            hintText = "The sticky note said something about bamboozled. I'll " +
                "try pushing all of the bamboo.";
        else if(bambooMoved && !KeyObtained)
            hintText = "The sticky note said something about the middle one. I " +
                "think I need to look carefully where the middle bamboo was.";
        else if(KeyObtained)
            hintText = "I've got the key to freedom!";
            return hintText;
    }

    private void CheckBamboo()
    {
        if (inTrash && onTable)
        {
            // Unlock Bamboo
            bamboo.toggleRigidbody(false);
            Key.enabled = true;
            secondBamboo.gameObject.GetComponent<CursorHoverEffect>().isOn = true;
            fourthBamboo.gameObject.GetComponent<CursorHoverEffect>().isOn = true;
            bamboo2Audio.Play(); 
        }
        else
        {
            // Lock Bamboo
            bamboo.toggleRigidbody(true);
            Key.enabled = false;
        }
    }

    private void TipBamboo(float zAxis)
    {   
        smallBamboo.transform.eulerAngles = smallBamboo.transform.eulerAngles + new Vector3(0, 0, zAxis);
        smallBambooAudio.Play();
    }

}
