using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRInteract : MonoBehaviour
{
    [SerializeField] //this is just for private variables to show up in inspector window
    private GameObject controller; //assign this to player camera in inspector window

    [SerializeField]
    private float distance = 3f; //distance the ray extends

    [SerializeField]
    private LayerMask mask; //assign this to a layer in inspector window

    private PlayerController playerController;

    InputActions inputActions;//this refers to the player input, which we can use to access interact key (E or left click)

    public GameObject testObject;

    private void Awake()
    {
        inputActions = new InputActions(); //initializes player input
    }

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputActions.Player.Interact.IsPressed()) //if player presses interact key
        {
            testObject.SetActive(false);
        }

        playerController.UpdateText(string.Empty); //empties the string if not looking at interactable

        //create ray at center of camera, shooting outwards
        Ray ray = new Ray(controller.transform.position, controller.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance); //this is only visible in scene window when game is playing
        RaycastHit hitInfo; //variable to store collision information

        if (Physics.Raycast(ray, out hitInfo, distance, mask)) //this returns a bool on whether you hit something
        {
            testObject.SetActive(true);
            if (hitInfo.collider.GetComponent<Interactable>() != null) //if the hit object is interactable
            {
                //puts the interactable object in a variable
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();

                //show the prompt message for the respective interactable object
                playerController.UpdateText(interactable.promptMessage);

                if (inputActions.Player.Interact.IsPressed()) //if player presses interact key
                {
                    Debug.Log("Player interacted with the object");
                    interactable.BaseInteract(); //this is going into Interactable, and then calling Interact(), which is overriden in every interactable object's script
                    //from this point forward, the functionality will be whatever is written in the Interact() subclasses
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
