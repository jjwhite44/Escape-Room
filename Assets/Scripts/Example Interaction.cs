using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExampleInteraction : MonoBehaviour
{
    public GameObject interactionObject; //assign this in the inspector window

    InputActions inputActions; //this refers to the player input, which we can use to access interact key (E or left click)

    public UnityEvent myAction; //needs "using UnityEngine.Events;"

    public DoorController doorController;

    private void Awake()
    {
        inputActions = new InputActions(); //initializes player input
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player")) //when the player approaches the object
        {
            Debug.Log("Player entered the space");
            //perform action here, since as displaying a screen
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.CompareTag("Player")) //when the player walks away from the object
        {
            Debug.Log("Player exited the space");
            //perform action here, since as removing a displayed screen
        }
    }

    private float inputDelay = 0.4f; //change this value to whatever time the action occuring necessitates
    private float timer = 0; //leave this value alone, this is just to start counting at 0

    private void OnTriggerStay(Collider other) //refers to when the player remains inside the trigger space
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timer += Time.deltaTime; //track passing game time
            if (timer >= inputDelay)
            {
                //Player refers to action map, Interact refers to interact key (E). Can add to this in Unity
                //and would just replace the "Interact" with whatever you name that key in the action system
                if (inputActions.Player.Interact.IsPressed())
                {
                    Debug.Log("Player interacted with the object");
                    //perform whatever actions here
                    myAction.Invoke(); //use this method to use built-in stuff, such as making an object
                                       //disappear by setting isActive to false. Need to assign things in inspector window
                    doorController.ToggleDoor(); //use this method to call other scripts
                    timer = 0; //reset timer for input delay
         
                }
            }
        }
    }

    //in order to enable and disable these controls:
    //note: THESE ARE REQUIRED FOR INPUT ACTIONS TO WORK
    public void OnEnable()
    {
        inputActions.Player.Enable(); //here Player is referring to the name of the action map in Unity
    }

    public void OnDisable()
    {
        inputActions.Player.Disable(); //here Player is referring to the name of the action map in Unity
    }
}
