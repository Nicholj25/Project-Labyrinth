using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class CabinetSwingClosed : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public CameraHandler cameraHandler;
    Animator cabinetAnim;
    public bool closed;
    public bool animationActive;

    // Start is called before the first frame update
    void Start()
    {
        cabinetAnim = gameObject.GetComponent<Animator>();
        closed = true;
        animationActive = false;
    }

    // Update is called once per frame
    async void Update()
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
                            cabinetAnim.enabled = true;
                            animationActive = true;
                            cabinetAnim.Play("CabinetSwing2");
                            closed = false;
                            await WaitOneSecondAsync(1);
                            animationActive = false;
                        }
                        else
                        {
                            animationActive = true;
                            cabinetAnim.Play("CabinetSwingClosed2");
                            closed = true;
                            await WaitOneSecondAsync(1);
                            animationActive = false;
                        }
                    }
                }
            }
        }
    }

    // Add a short delay
    public async Task WaitOneSecondAsync(double seconds)
    {
        await Task.Delay(TimeSpan.FromSeconds(seconds));
    }
}
