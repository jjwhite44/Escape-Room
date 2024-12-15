using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private List<Scalable> scalableObjects = new List<Scalable>();

    private void OnTriggerEnter(Collider other)
    {
        Scalable scalable = other.GetComponent<Scalable>();
        if (scalable != null && !scalableObjects.Contains(scalable))
        {
            scalableObjects.Add(scalable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Scalable scalable = other.GetComponent<Scalable>();
        if (scalable != null && scalableObjects.Contains(scalable))
        {
            scalableObjects.Remove(scalable);
        }
    }

    public List<Scalable> GetScalableObjects()
    {
        return scalableObjects;
    }
}
