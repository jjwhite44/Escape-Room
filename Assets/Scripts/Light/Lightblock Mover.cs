using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightblockMover : Interactable
{
    public GameObject lightBlocker;

    public float shiftAmountX; //whatever shift is necessary to move light blocker
    public float shiftAmountY;
    public float shiftAmountZ;

    [SerializeField]
    private float inputDelay = 0.1f; //change this value to whatever time the action occuring necessitates

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
            lightBlocker.transform.position += new Vector3(shiftAmountX, shiftAmountY, shiftAmountZ);
            shiftAmountX *= -1;
            shiftAmountY *= -1;
            shiftAmountZ *= -1;
            timer = 0; //reset timer for input delay
        }
    }
}
