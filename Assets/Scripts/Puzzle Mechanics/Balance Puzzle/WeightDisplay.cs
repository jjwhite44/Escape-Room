using UnityEngine;
using TMPro;
using System.Collections.Generic;

//note: only works for blocks actually touching scale
public class WeightDisplay : MonoBehaviour
{
    public TextMeshProUGUI weightTextTMP; // Reference to the TextMeshProUGUI component
    //public string weightPrefix = "Weight: "; // Prefix text before the weight value

    private List<Rigidbody> objectsOnScale = new List<Rigidbody>(); // List to hold all objects currently on the scale

    private void Start()
    {
        if (weightTextTMP == null)
        {
            Debug.LogError("Weight Text is not assigned!");
        }
    }

    private void Update()
    {
        float weight = CalculateWeight();
        DisplayWeight(weight);
    }

    private float CalculateWeight()
    {
        float totalWeight = 0f;

        foreach (Rigidbody rb in objectsOnScale)
        {
            totalWeight += rb.mass;
        }

        return totalWeight;
    }

    private void DisplayWeight(float weight)
    {
        if (weightTextTMP != null)
        {
            weightTextTMP.text = weight.ToString("F2") + " kg";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && !objectsOnScale.Contains(rb))
        {
            objectsOnScale.Add(rb);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && objectsOnScale.Contains(rb))
        {
            objectsOnScale.Remove(rb);
        }
    }
}
