using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LightInteract : MonoBehaviour
{
    [SerializeField] //this is just for private variables to show up in inspector window
    private Light lightSource; //assign this to the light source itself (because its Light type, not GameObject)

    [SerializeField]
    private float distance = 3f; //distance the ray extends

    [SerializeField]
    private LayerMask mask; //assign this to a layer in inspector window that you want to interact with

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //create ray at center of light source, shooting outwards
        Ray ray = new Ray(lightSource.transform.position, lightSource.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance); //this is only visible in scene window when game is playing
        RaycastHit hitInfo; //variable to store collision information

        if (Physics.Raycast(ray, out hitInfo, distance, mask)) //this returns a bool on whether you hit something
        {
            if (hitInfo.collider.GetComponent<LightReactive>() != null) //if the hit object is interactable
            {
                LightReactive lightReactive = hitInfo.collider.GetComponent<LightReactive>();
                lightReactive.BaseReact(); //this is going into Light Reactive, and then calling React(), which is overriden in every light reactive object's script
                //from this point forward, the functionality will be whatever is written in the React() subclasses
            }
        }
    }
}
