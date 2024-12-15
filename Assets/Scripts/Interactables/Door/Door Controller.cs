using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorController : MonoBehaviour
{
    public GameObject door;
    public float openRot, closeRot, speed;
    public bool opening;
    private OpenCondition conditionMet;

    private void Start()
    {
        conditionMet = GameObject.Find("Test Door").GetComponent<OpenCondition>();
    }

    void Update()
    {
        Vector3 currentRot = door.transform.localEulerAngles;
        if (opening)
        {
            if (currentRot.y < openRot)
            {
                door.transform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x, openRot, currentRot.z), speed * Time.deltaTime);
            }
        }
        else
        {
            if (currentRot.y > closeRot)
            {
                door.transform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x, closeRot, currentRot.z), speed * Time.deltaTime);
            }
        }
    }


    public void ToggleDoor()
    {
        if (conditionMet.ConditionMet())
        {
            opening = !opening;
        }
    }
}