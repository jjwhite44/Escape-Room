using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable //note: this is inheriting from interactable class
{
    [SerializeField]
    private GameObject key;

    //this function is where we will design our interaction
    protected override void Interact()
    {
        key.SetActive(false);
    }

    public bool isActive() //method to determine if the key has been collected (not active if collected)
    {
        return key.activeSelf;
    }

}
