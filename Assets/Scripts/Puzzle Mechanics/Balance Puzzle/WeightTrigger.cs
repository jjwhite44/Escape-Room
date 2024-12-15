using UnityEngine;

public class WeightTrigger : MonoBehaviour
{
    public ScaleManager scaleManager; // Reference to the ScaleManager script
    public bool isPlatform1; // Indicates if this trigger is for Platform 1 or Platform 2

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
        {
            if (isPlatform1)
            {
                scaleManager.AddWeightToPlatform1(rb.mass);
            }
            else
            {
                scaleManager.AddWeightToPlatform2(rb.mass);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
        {
            if (isPlatform1)
            {
                scaleManager.RemoveWeightFromPlatform1(rb.mass);
            }
            else
            {
                scaleManager.RemoveWeightFromPlatform2(rb.mass);
            }
        }
    }
}
