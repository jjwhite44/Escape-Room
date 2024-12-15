using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformInteract : Interactable
{
    PlatformController platformController;

    private void Awake()
    {
        platformController = FindObjectOfType<PlatformController>();
    }

protected override void Interact()
    {
        Debug.Log("interact");
        platformController.Interact();
    }
}
