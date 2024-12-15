using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : Interactable
{
    public DoorController doorController;

    [SerializeField]
    private float inputDelay = 0.4f; //change this value to whatever time the action occuring necessitates
    [SerializeField]
    private float timer = 0; //leave this value alone, this is just to start counting at 0

    private void Update() //important to update time here, not in Interact. Interact only runs when player is actively interacting with object
    {
        timer += Time.deltaTime;
    }

    protected override void Interact()
    {
        if (timer >= inputDelay)
        {
            //perform whatever actions here
            doorController.ToggleDoor(); //use this method to call other scripts
            timer = 0; //reset timer for input delay
        }
    }
}
