using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cipher : MonoBehaviour
{
    private float rotationAngle = 360f / 26f;

    public void Rotate(GameObject targetRing)
    {
        targetRing.transform.Rotate(rotationAngle, 0, 0);
    }
}
