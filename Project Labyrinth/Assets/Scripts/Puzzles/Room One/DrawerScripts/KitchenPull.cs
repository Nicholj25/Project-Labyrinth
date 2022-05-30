using UnityEngine;
using System.Collections;

public class KitchenPull : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public CameraHandler cameraHandler;
    Animator cabinetPullAnim;
    public bool closed;
    public bool animationActive;
    public string drawerNum;

    // Start is called before the first frame update
    void Start()
    {
        cabinetPullAnim = gameObject.GetComponent<Animator>();
        closed = true;
        animationActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main)
        {
            if(playerMovement.isNearby(this.gameObject) && Input.GetMouseButtonDown(0))
            {
                if (animationActive == false){
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    Physics.Raycast(ray, out hit);
                    if (hit.transform.gameObject == this.gameObject)
                    {
                        if (closed)
                        {
                            cabinetPullAnim.enabled = true;
                            animationActive = true;
                            cabinetPullAnim.Play("KitchenPullDrawer" + drawerNum);
                            closed = !closed;
                            animationActive = !animationActive;
                        }
                        else
                        {
                            animationActive = true;
                            cabinetPullAnim.Play("KitchenPushDrawer" + drawerNum);
                            closed = !closed;
                            animationActive = !animationActive;

                        }
                    }
                }
            }
        }
    }
}
