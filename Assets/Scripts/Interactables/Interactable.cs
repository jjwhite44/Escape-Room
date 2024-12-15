using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//note: since this is an abstract class its not actually used on an object, just a template for subclasses
public abstract class Interactable : MonoBehaviour
{
    //message displayed when player looks at interactable
    //once subclass of this class is assigned to object (i.e. subclass "Key")
    //you must enter a prompt message in inspector window
    public string promptMessage;
    public string errorMessage;

    //this function will be called from our player
    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {
        //won't have any code written here, this will be overridden by subclasses
    }
}
