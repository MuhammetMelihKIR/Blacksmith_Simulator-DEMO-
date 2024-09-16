using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerInput playerInput;

    #region Camera Movement Variables

    [Header("Camera Settings")]
    public Camera playerCamera;

    public float fov = 60f;
    public bool cameraCanMove = true;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 50f;

    [Header("Crosshair Settings")]
    public bool lockCursor = true;
    public bool crosshair = true;
    public Sprite crosshairImage;
    public Color crosshairColor = Color.white;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private Image crosshairObject;

    #endregion

    #region Movement Variables

    [Header("Movement Settings")]
    public bool playerCanMove = true;
    public float walkSpeed = 5f;
    public float maxVelocityChange = 10f;
    
    private Vector2 lookInput;
    private Vector2 moveInput;

    #endregion

    private bool isInteract;
    

    #region ON ENABLE / DISABLE

    private void OnEnable()
    {
        playerInput.Enable();

        playerInput.Player.Forge.performed += HammerHit; 
        
        CoreGameSignals.OnInteractObjectControl += OnIsTrigger;
        CoreGameSignals.OnCursorLockState+= OnCursorLockState;
        CoreGameSignals.OnPlayerCanMove += OnPlayerCanMove;
        CoreGameSignals.Player_OnPlayerCameraRotate += OnPlayerCameraRotate;
    }

    private void HammerHit(InputAction.CallbackContext obj)
    {
        CoreGameSignals.PlayerController_Forge?.Invoke();
    }
    private void OnIsTrigger(bool isTrigger)
    {
        this.isInteract = isTrigger;
    }
    private void OnPlayerCanMove(bool canMove)
    {
       playerCanMove = canMove;
    } private void OnPlayerCameraRotate(bool canRotate)
    {
        cameraCanMove = canRotate;
    }
    private void OnCursorLockState(CursorLockMode mode)
    {
        Cursor.lockState = mode;
    }
    private void OnDisable()
    {
        playerInput.Disable();
        
        playerInput.Player.Forge.performed -= HammerHit; 
        
        CoreGameSignals.OnInteractObjectControl -= OnIsTrigger;
        CoreGameSignals.OnCursorLockState -= OnCursorLockState;
        CoreGameSignals.Player_OnPlayerCameraRotate -= OnPlayerCameraRotate;
        CoreGameSignals.OnPlayerCanMove -= OnPlayerCanMove;
    }

    #endregion
    
    private void OnInteract()
    {
        if (isInteract)
        {
            CoreGameSignals.OnInteractObject?.Invoke();
        }
    }
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        crosshairObject = GetComponentInChildren<Image>();

        // Set internal variables
        playerCamera.fieldOfView = fov;

        // Input Actions Initialization
        playerInput = new PlayerInput();

        playerInput.Player.Look.performed += context => lookInput = context.ReadValue<Vector2>();
        playerInput.Player.Move.performed += context => moveInput = context.ReadValue<Vector2>();
        playerInput.Player.Move.canceled += context => moveInput = Vector2.zero;
    }
    void Start()
    {
        if (lockCursor)
        {
            CoreGameSignals.OnCursorLockState?.Invoke(CursorLockMode.Locked);
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
        
        OnCursorLockState(CursorLockMode.None);
    }

    void FixedUpdate()
    {
        #region Movement

        if (playerCanMove)
        {
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(moveInput.x, 0, moveInput.y);
            targetVelocity = transform.TransformDirection(targetVelocity) * walkSpeed;
            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = rb.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }

        #endregion
    }

    private void LateUpdate()
    {
        #region Camera

        if (cameraCanMove)
        {
            lookInput = playerInput.Player.Look.ReadValue<Vector2>();
            yaw += lookInput.x * mouseSensitivity;
            pitch -= lookInput.y * mouseSensitivity;
            // Clamp pitch between lookAngle
            pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

            transform.localEulerAngles = new Vector3(0, yaw, 0);
            playerCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        }

        #endregion
    }

   
}
