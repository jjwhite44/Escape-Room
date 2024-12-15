using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    public GameObject magnet; //assign the magnet in inspector window
    public float magneticForce;

    Rigidbody magneticObject;

    // Start is called before the first frame update
    void Start()
    {
        magneticObject = GetComponent<Rigidbody>(); //gets rigidbody from the gameobject this script is attached to (make sure that object has rigidbody)
    }

    // Update is called once per frame
    void Update()
    {
        //pull magnetic object towards magnet
        magneticObject.AddForce((magnet.transform.position - transform.position) * magneticForce * Time.smoothDeltaTime);
    }
}
