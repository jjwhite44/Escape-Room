using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    private bool onTarget;

    private void OnTriggerEnter(Collider other)
    {
        onTarget = true;
    }

    private void OnTriggerExit(Collider other)
    {
        onTarget = false;
    }

    public bool GetOnTarget()
    {
        return onTarget;
    }
}
