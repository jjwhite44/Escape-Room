using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOTE: this script should be applied to the actual capsule holding the empty gameobject and the light source itself,
//as the actual capsule is what gets hit by the raycast
public class LightSource : LightReactive
{
    [SerializeField]
    private GameObject lightSource; //assign this to an empty gameObject that is parent to actual light source (because it is GameObject type, not Light type)

    //this function is where we will design our interaction
    protected override void React()
    {
        lightSource.SetActive(true);
    }

    public bool isActive()
    {
        return lightSource.activeSelf;
    }
}
