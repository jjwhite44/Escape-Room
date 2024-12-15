using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class PlatformController : MonoBehaviour
{
    [SerializeField]
    private GameObject platform;

    [SerializeField]
    private float maxRotationX = 45.0f;
    [SerializeField]
    private float maxRotationZ = 45.0f;
    [SerializeField]
    private float speed = 10.0f;

    [SerializeField]
    private float xRotation;
    [SerializeField]
    private float zRotation;

    private bool playing = false;

    InputActions inputActions; //this refers to the player input, which we can use to access interact key (E or left click)

    FPController firstPersonController;

    //[SerializeField]
    //private Camera playerPOVCamera;

    //[SerializeField]
    //private Camera platformGameCamera;

    [SerializeField]
    private Canvas playerCanvas;

    private bool canPlay;

    private void Awake()
    {
        inputActions = new InputActions(); //initializes player input
        firstPersonController = FindObjectOfType<FPController>();
    }

    public void Interact()
    {
        Debug.Log("control");
        if (canPlay)
        {
            playing = true;
            ////shift cameras
            //playerPOVCamera.gameObject.SetActive(false);
            //platformGameCamera.gameObject.SetActive(true);
            ////disable player actions
            firstPersonController.SetPlayerMovement(false);
            //firstPersonController.cameraCanMove = false;
            //disable player canvas
            playerCanvas.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    if (playing == true)
    //    {
    //        //xRotation = Input.GetAxis("Vertical");
    //        //zRotation = -Input.GetAxis("Horizontal");

    //        //Vector3 currentRot = platform.transform.localEulerAngles;

    //        //// Convert angles to a range of -180 to 180 degrees
    //        //if (currentRot.x > 180) currentRot.x -= 360;
    //        //if (currentRot.z > 180) currentRot.z -= 360;

    //        //float targetRotationX = Mathf.Clamp(currentRot.x + xRotation * speed * Time.deltaTime, -maxRotationX, maxRotationX);
    //        //float targetRotationZ = Mathf.Clamp(currentRot.z + zRotation * speed * Time.deltaTime, -maxRotationZ, maxRotationZ);

    //        //platform.transform.localEulerAngles = new Vector3(targetRotationX, currentRot.y, targetRotationZ);

    //        //if (inputActions.Player.Exit.IsPressed()) //if player presses exit key (esc, will need to bind others for VR/AR)
    //        //{
    //        //    playing = false;
    //        //    //shift camera back
    //        //    playerPOVCamera.gameObject.SetActive(true);
    //        //    platformGameCamera.gameObject.SetActive(false);
    //        //    //enable player actions
    //        //    firstPersonController.playerCanMove = true;
    //        //    firstPersonController.cameraCanMove = true;
    //        //    //enable player canvas
    //        //    playerCanvas.gameObject.SetActive(true);
    //        //}

    //        // Read inputs
    //        Vector2 movementInput = inputActions.Player.Navigate.ReadValue<Vector2>();

    //        xRotation = movementInput.y;
    //        zRotation = -movementInput.x;

    //        Vector3 currentRot = platform.transform.localEulerAngles;

    //        // Convert angles to a range of -180 to 180 degrees
    //        if (currentRot.x > 180) currentRot.x -= 360;
    //        if (currentRot.z > 180) currentRot.z -= 360;

    //        float targetRotationX = Mathf.Clamp(currentRot.x + xRotation * speed * Time.deltaTime, -maxRotationX, maxRotationX);
    //        float targetRotationZ = Mathf.Clamp(currentRot.z + zRotation * speed * Time.deltaTime, -maxRotationZ, maxRotationZ);

    //        platform.transform.localEulerAngles = new Vector3(targetRotationX, currentRot.y, targetRotationZ);

    //        if (inputActions.Player.Exit.triggered) // Check if the exit action is triggered
    //        {
    //            playing = false;
    //            ////shift camera back
    //            //playerPOVCamera.gameObject.SetActive(true);
    //            //platformGameCamera.gameObject.SetActive(false);
    //            ////enable player actions
    //            //firstPersonController.playerCanMove = true;
    //            //firstPersonController.cameraCanMove = true;
    //            //enable player canvas
    //            playerCanvas.gameObject.SetActive(true);
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) //when the player approaches the object
        {
            Debug.Log("Player entered the space");
            canPlay = true;
            //perform action here, since as displaying a screen
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) //when the player walks away from the object
        {
            Debug.Log("Player exited the space");
            canPlay = false;
            //perform action here, since as removing a displayed screen

            playing = false;
            ////shift camera back
            //playerPOVCamera.gameObject.SetActive(true);
            //platformGameCamera.gameObject.SetActive(false);
            ////enable player actions
            firstPersonController.SetPlayerMovement(true);
            //firstPersonController.cameraCanMove = true;
            //enable player canvas
            playerCanvas.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (playing == true)
            {
                // Read inputs
                Vector2 movementInput = inputActions.Player.Navigate.ReadValue<Vector2>();

                xRotation = movementInput.y;
                zRotation = -movementInput.x;

                Vector3 currentRot = platform.transform.localEulerAngles;

                // Convert angles to a range of -180 to 180 degrees
                if (currentRot.x > 180) currentRot.x -= 360;
                if (currentRot.z > 180) currentRot.z -= 360;

                float targetRotationX = Mathf.Clamp(currentRot.x + xRotation * speed * Time.deltaTime, -maxRotationX, maxRotationX);
                float targetRotationZ = Mathf.Clamp(currentRot.z + zRotation * speed * Time.deltaTime, -maxRotationZ, maxRotationZ);

                platform.transform.localEulerAngles = new Vector3(targetRotationX, currentRot.y, targetRotationZ);

                if (inputActions.Player.Exit.IsPressed()) // Check if the exit action is triggered
                {
                    Debug.Log("exit");
                    playing = false;
                    ////shift camera back
                    //playerPOVCamera.gameObject.SetActive(true);
                    //platformGameCamera.gameObject.SetActive(false);
                    ////enable player actions
                    firstPersonController.SetPlayerMovement(true);
                    //firstPersonController.cameraCanMove = true;
                    //enable player canvas
                    playerCanvas.gameObject.SetActive(true);
                }
            }
        }
    }

    public void OnEnable()
    {
        inputActions.Player.Enable(); //here Player is referring to the name of the action map in Unity
    }

    public void OnDisable()
    {
        inputActions.Player.Disable(); //here Player is referring to the name of the action map in Unity
    }
}