using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    InputActions inputActions; //this refers to the player input, which we can use to access flashlight key (F)

    [SerializeField]
    private GameObject flashlight;
    bool flashlightOn = false;

    [SerializeField]
    private float inputDelay = 0.25f; //change this value to whatever time the action occuring necessitates
    [SerializeField]
    private float timer = 0; //leave this value alone, this is just to start counting at 0

    private void Awake()
    {
        inputActions = new InputActions(); //initializes player input
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (inputActions.Player.Flashlight.IsPressed())
        {
            if (timer >= inputDelay)
            {
                ToggleFlashlight();
                timer = 0;
            }
        }
    }

    private void ToggleFlashlight()
    {
        flashlightOn = !flashlightOn;
        flashlight.SetActive(flashlightOn);
    }

    [SerializeField]
    private TextMeshProUGUI promptText;

    public void UpdateText(string promptMessage) //for "pick up" etc
    {
        promptText.text = promptMessage;
    }

    [SerializeField]
    private TextMeshProUGUI errorText;

    public void UpdateErrorText(string errorMessage) //for "locked" etc
    {
        errorText.text = errorMessage;
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
