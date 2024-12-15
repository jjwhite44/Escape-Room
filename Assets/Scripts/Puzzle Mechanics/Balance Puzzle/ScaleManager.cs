using UnityEngine;

public class ScaleManager : MonoBehaviour
{
    public GameObject platform1; // Reference to the first platform
    public GameObject platform2; // Reference to the second platform
    public float moveSpeed = 1f; // The speed at which the platforms move
    public float maxHeightDifference = 5f; // The maximum height difference between the two platforms

    private float weight1 = 0f; // The current weight on the first platform
    private float weight2 = 0f; // The current weight on the second platform
    private Vector3 initialPosition1; // The initial position of the first platform
    private Vector3 initialPosition2; // The initial position of the second platform

    private void Start()
    {
        initialPosition1 = platform1.transform.position;
        initialPosition2 = platform2.transform.position;
    }

    private void FixedUpdate()
    {
        AdjustPlatformHeights();
    }

    private void AdjustPlatformHeights()
    {
        float weightDifference = weight1 - weight2;
        float heightDifference = Mathf.Clamp(weightDifference, -maxHeightDifference, maxHeightDifference);

        Vector3 targetPosition1 = initialPosition1 - new Vector3(0, heightDifference, 0);
        Vector3 targetPosition2 = initialPosition2 + new Vector3(0, heightDifference, 0);

        platform1.transform.position = Vector3.MoveTowards(platform1.transform.position, targetPosition1, moveSpeed * Time.deltaTime);
        platform2.transform.position = Vector3.MoveTowards(platform2.transform.position, targetPosition2, moveSpeed * Time.deltaTime);
    }

    public void AddWeightToPlatform1(float weight)
    {
        weight1 += weight;
    }

    public void RemoveWeightFromPlatform1(float weight)
    {
        weight1 -= weight;
    }

    public void AddWeightToPlatform2(float weight)
    {
        weight2 += weight;
    }

    public void RemoveWeightFromPlatform2(float weight)
    {
        weight2 -= weight;
    }
}
