using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CipherRingButton : Interactable
{
    [SerializeField]
    private GameObject targetRing;

    [SerializeField]
    private float inputDelay = 0.1f; //change this value to whatever time the action occuring necessitates
    [SerializeField]
    private float timer = 0; //leave this value alone, this is just to start counting at 0

    private Cipher cipher;

    private void Start()
    {
        cipher = GameObject.Find("Cipher").GetComponent<Cipher>();
    }

    private void Update() //important to update time here, not in Interact. Interact only runs when player is actively interacting with object
    {
        timer += Time.deltaTime;
    }

    protected override void Interact()
    {
        if (timer >= inputDelay)
        {
            //perform whatever actions here
            cipher.Rotate(targetRing); //use this method to call other scripts
            timer = 0; //reset timer for input delay
        }
    }
}
