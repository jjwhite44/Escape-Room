using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FPController : MonoBehaviour
{
    private Rigidbody rb;

    #region Camera Movement Variables

    public Camera playerCamera;

    public float fov = 60f;
    public bool invertCamera = false;
    public bool cameraCanMove = true;
    public float mouseSensitivity = 1f;
    public float maxLookAngle = 50f;

    // Crosshair
    public bool lockCursor = true;
    public bool crosshair = true;
    public Sprite crosshairImage;
    public Color crosshairColor = Color.white;

    // Internal Variables
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private Image crosshairObject;

    // #region Camera Zoom Variables

    // public bool enableZoom = true;
    // public bool holdToZoom = false;
    // public InputActionReference zoomAction;
    // public float zoomFOV = 30f;
    // public float zoomStepTime = 5f;

    // // Internal Variables
    // private bool isZoomed = false;

    // #endregion
    #endregion

    #region Movement Variables

    public bool playerCanMove = true;
    public float walkSpeed = 5f;
    public float maxVelocityChange = 10f;

    // Internal Variables
    private bool isWalking = false;
    private Vector2 moveInput;

    // #region Jump

    // public bool enableJump = true;
    // public InputActionReference jumpAction;
    // public float jumpPower = 5f;

    // // Internal Variables
    // private bool isGrounded = false;

    // #endregion
    #endregion

    #region Head Bob

    //prob not good for VR
    public bool enableHeadBob = false;
    public Transform joint;
    public float bobSpeed = 10f;
    public Vector3 bobAmount = new Vector3(.15f, .05f, 0f);

    // Internal Variables
    private Vector3 jointOriginalPos;
    private float timer = 0;

    #endregion

    public InputActionReference moveAction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        crosshairObject = GetComponentInChildren<Image>();

        // Set internal variables
        playerCamera.fieldOfView = fov;
        jointOriginalPos = joint.localPosition;

        // Setup input actions
        // zoomAction.action.performed += ctx => ToggleZoom(ctx);
        // jumpAction.action.performed += ctx => Jump(ctx);
        moveAction.action.performed += ctx => OnMove(ctx);
        moveAction.action.canceled += ctx => OnMove(ctx);
    }

    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (crosshair)
        {
            crosshairObject.sprite = crosshairImage;
            crosshairObject.color = crosshairColor;
        }
        else
        {
            crosshairObject.gameObject.SetActive(false);
        }

        moveAction.action.Enable();
    }

    private void OnDestroy()
    {
        // zoomAction.action.performed -= ctx => ToggleZoom(ctx);
        // jumpAction.action.performed -= ctx => Jump(ctx);
        moveAction.action.performed -= ctx => OnMove(ctx);
        moveAction.action.canceled -= ctx => OnMove(ctx);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (playerCanMove)
        {
            moveInput = context.ReadValue<Vector2>();
        }
        else
        {
            moveInput = Vector2.zero;
        }
    }

    private void Update()
    {
        #region Camera

        // Control camera movement
        if (cameraCanMove)
        {
            yaw = transform.localEulerAngles.y + Mouse.current.delta.x.ReadValue() * mouseSensitivity;

            if (!invertCamera)
            {
                pitch -= mouseSensitivity * Mouse.current.delta.y.ReadValue();
            }
            else
            {
                // Inverted Y
                pitch += mouseSensitivity * Mouse.current.delta.y.ReadValue();
            }

            // Clamp pitch between lookAngle
            pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

            transform.localEulerAngles = new Vector3(0, yaw, 0);
            playerCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        }

        // #region Camera Zoom

        // // Lerps camera.fieldOfView to allow for a smooth transition
        // if (isZoomed)
        // {
        //     playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, zoomFOV, zoomStepTime * Time.deltaTime);
        // }
        // else
        // {
        //     playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, fov, zoomStepTime * Time.deltaTime);
        // }

        // #endregion
        #endregion

        // CheckGround();

        if (enableHeadBob)
        {
            HeadBob();
        }
    }

    void FixedUpdate()
    {
        #region Movement

        if (playerCanMove)
        {
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(moveInput.x, 0, moveInput.y);

            // Checks if player is walking and isGrounded
            // Will allow head bob
            if ((targetVelocity.x != 0 || targetVelocity.z != 0) /* && isGrounded */)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }

            targetVelocity = transform.TransformDirection(targetVelocity) * walkSpeed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = rb.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;

            // Player is only moving when velocity change != 0
            // Makes sure that player is grounded
            if ((velocityChange.x != 0 || velocityChange.z != 0) /* && isGrounded */)
            {
                rb.AddForce(velocityChange, ForceMode.VelocityChange);
            }
        }

        #endregion
    }

    // Method to enable or disable player movement
    public void SetPlayerMovement(bool canMove)
    {
        playerCanMove = canMove;
    }

    // void ToggleZoom(InputAction.CallbackContext context)
    // {
    //     if (enableZoom)
    //     {
    //         if (holdToZoom)
    //         {
    //             isZoomed = context.ReadValue<float>() > 0;
    //         }
    //         else
    //         {
    //             isZoomed = !isZoomed;
    //         }
    //     }
    // }

    // void Jump(InputAction.CallbackContext context)
    // {
    //     if (enableJump && isGrounded)
    //     {
    //         rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    //     }
    // }

    // private void CheckGround()
    // {
    //     // Simple way to check if player is on the ground
    //     // Can be improved depending on your use case
    //     isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
    // }

    private void HeadBob()
    {
        if (isWalking)
        {
            timer += Time.deltaTime * bobSpeed;
            joint.localPosition = new Vector3(jointOriginalPos.x + Mathf.Sin(timer) * bobAmount.x, jointOriginalPos.y + Mathf.Sin(timer) * bobAmount.y, jointOriginalPos.z + Mathf.Sin(timer) * bobAmount.z);
        }
        else
        {
            timer = 0;
            joint.localPosition = new Vector3(Mathf.Lerp(joint.localPosition.x, jointOriginalPos.x, Time.deltaTime * bobSpeed), Mathf.Lerp(joint.localPosition.y, jointOriginalPos.y, Time.deltaTime * bobSpeed), Mathf.Lerp(joint.localPosition.z, jointOriginalPos.z, Time.deltaTime * bobSpeed));
        }
    }
}
