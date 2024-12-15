using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class LockpickController : MonoBehaviour
{
    [SerializeField]
    private GameObject slider;

    [SerializeField]
    private GameObject targetPrefab;

    [SerializeField]
    private float leftBoundary = -5;
    [SerializeField]
    private float rightBoundary = 5;

    [SerializeField]
    private float sliderSpeed = 5;

    private bool movingRight;

    //private bool onTarget;

    InputActions inputActions; //this refers to the player input, which we can use to access interact key (E or left click)

    private float currentLevel = 1;

    [SerializeField]
    private float targetXMin = -4.45f;
    [SerializeField]
    private float targetXMax = 4.45f; // Range for the new target's X coordinate

    private GameObject currentTarget;

    private float inputDelay = 0.4f; //change this value to whatever time the action occuring necessitates
    [SerializeField]
    private float timer = 0; //leave this value alone, this is just to start counting at 0

    HitDetection hitDetection;

    private void Awake()
    {
        inputActions = new InputActions(); //initializes player input
        hitDetection = GetComponent<HitDetection>();
    }

    private void Start()
    {
        TargetSpawner();
    } 

    // Update is called once per frame
    void Update()
    {
        MoveSlider();

        timer += Time.deltaTime;
        if (timer >= inputDelay)
        {
            if (inputActions.Player.Interact.IsPressed())
            {
                if (hitDetection.GetOnTarget())
                {
                    Debug.Log("hit");
                    currentLevel++;
                    SetSpeed();
                    TargetSpawner();
                }

                else
                {
                    Debug.Log("miss");
                    currentLevel = 1;
                    SetSpeed();
                }
                timer = 0;
            }
            
        }
    }

    private void SetSpeed()
    {
        sliderSpeed = 3 + (currentLevel * 4);
    }

    private void MoveSlider()
    {
        float step = sliderSpeed * Time.deltaTime;

        if (movingRight)
        {
            slider.transform.Translate(Vector3.right * step);
            if (slider.transform.position.x >= rightBoundary)
            {
                movingRight = false;
            }
        }
        else
        {
            slider.transform.Translate(Vector3.left * step);
            if (slider.transform.position.x <= leftBoundary)
            {
                movingRight = true;
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    onTarget = true;
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    onTarget = false;
    //}

    private void TargetSpawner()
    {
        if (currentTarget != null)
        {
            Destroy(currentTarget);
        }

        float randomX = Random.Range(targetXMin, targetXMax);
        Vector3 newTargetPosition = new Vector3(randomX, slider.transform.position.y, slider.transform.position.z);

        Quaternion targetRotation = Quaternion.Euler(90f, 0f, 0f); // Rotate 90 degrees around the X axis

        currentTarget = Instantiate(targetPrefab, newTargetPosition, targetRotation);

        // Attach or get the HitDetection component from the new target
        hitDetection = currentTarget.GetComponent<HitDetection>();
        if (hitDetection == null)
        {
            hitDetection = currentTarget.AddComponent<HitDetection>();
        }
    }

    public void OnEnable()
    {
        inputActions.Player.Enable(); //here Player is referring to the name of the action map in Unity
    }

    public void OnDisable()
    {
        inputActions.Player.Disable(); //here Player is referring to the name of the action map in Unity
    }
}