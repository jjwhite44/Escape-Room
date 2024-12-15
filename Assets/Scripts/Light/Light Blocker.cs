using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlocker : LightReactive
{
    //[SerializeField]
    //private GameObject lightBlocker;

    [SerializeField] //note: set this to the light source that this light blocker is blocking
    private GameObject lightSource; //assign this to an empty gameObject that is parent to actual light source (because it is GameObject type, not Light type)

    //this function is where we will design our interaction
    protected override void React()
    {
        lightSource.SetActive(false);
    }

    //public bool isActive()
    //{
    //    return lightBlocker.activeSelf;
    //}
}
