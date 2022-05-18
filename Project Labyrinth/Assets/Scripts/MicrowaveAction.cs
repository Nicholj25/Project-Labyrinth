using UnityEngine;

public class MicrowaveAction : MonoBehaviour
{
    [SerializeField] private Vector3 eulerRotation;
    private bool isOpen;
    private bool isLocked;
    private Keypad keypad;
    // Start is called before the first frame update
    void Start()
    {
        keypad = GameObject.Find("Keypad").GetComponent<Keypad>();
        keypad.SuccessfulEntry.AddListener(() => { isOpen = true; isLocked = false; });
        isOpen = false;
        isLocked = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(!isLocked)
            OpenDoor();
    }

    public void OpenDoor()
    {
        if (isOpen)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z) - eulerRotation;
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z) + eulerRotation;
        }

        isOpen = !isOpen;
    }
}
