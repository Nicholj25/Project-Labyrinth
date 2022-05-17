using UnityEngine;

public class MicrowaveAction : MonoBehaviour
{
    [SerializeField] private Vector3 eulerRotation;
    private bool isOpen;
    private bool isLocked;
    // Start is called before the first frame update
    void Start()
    {
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
