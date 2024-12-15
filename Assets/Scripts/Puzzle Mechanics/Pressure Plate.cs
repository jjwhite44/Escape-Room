using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject pressurePlate; //make sure this has box collider and is trigger checked
    //note: need to make box collider continue a bit above the pressure plate, or when it goes down
    //the player will exit the collider and it will continously try to rise back up

    private Vector3 startingPosition;

    public float shiftAmountX; //whatever shift is necessary to press pressure plate down (typically just Y)
    public float shiftAmountY;
    public float shiftAmountZ;

    private void Start()
    {
        startingPosition = pressurePlate.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        pressurePlate.transform.position += new Vector3 (shiftAmountX, shiftAmountY, shiftAmountZ);
        //perform whatever action here
    }

    //if you want pressure plate to stay down when pressed for the first time, or at least don't want action to
    //revert when getting off pressure plate, just don't add reversal logic here
    private void OnTriggerExit(Collider other)
    {
        pressurePlate.transform.position = startingPosition;
        //revert whatever action here
    }
}
