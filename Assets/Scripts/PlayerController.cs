using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;
using System.Collections;


public class PlayerController : MonoBehaviour
{
    // Floats
    [SerializeField] private float gravity;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float _sensitivity;
    [SerializeField] private float worldBottomBoundary = -100f;

    private float moveSpeed;
    private float _stamina = 50;
    private float _pitchX;
    private float _pitchY;

    public GameObject Player;
    
    // Transforms
    [SerializeField] private Transform cameraTransform;

    // Vectors
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private Vector2 sprintInput;
    private Vector3 crouchScale;
    private Vector3 standScale;
    private Vector3 timeHopPos;
    public Vector3 firstTimeline;
    public Vector3 secondTimeline;
    (Vector3, Quaternion) initialPositionAndRotation;
    

    private InputAction sprintAction;
    private bool isSprinting = false;
    bool hanging;
    private bool isCrouching;
    private bool canTimeHop;
    private bool timelineDif;
    private bool switchTimeline;


    // Components
    private Rigidbody _rb;
    private CharacterController controller;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        initialPositionAndRotation = (transform.position, transform.rotation);

    }

    // Player Movement
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log($"Move Input: {moveInput}");
    }

    // Sprinting
    public void onSprint(InputAction.CallbackContext context)
    {
        Debug.Log($"Sprinting {context.performed}");
        if (context.started)
        {
            isSprinting = true;
        }
        else if (context.canceled)
        {
            isSprinting = false;
        }
    }

    // Jumping
    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log($"Jumping {context.performed} - Is Grounded: {controller.isGrounded}");
        if (context.performed && controller.isGrounded)
        {
            Debug.Log("We are supposed to jump");
            if (hanging)
            {
                _rb.useGravity = true;
                hanging = false;

                velocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
                
            }
            else
            {
                velocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            }

        }
    }
    // Crouching
    public void onCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isCrouching = true;
        }
        else if (context.canceled)
        {
            isCrouching = false;
        }
    }

    // Time swap mechanic
    public void onTimeSwap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log($"TimeSwap {context.performed}");
            canTimeHop = true;
        }
    }

    // Camera controls
    public void OnMouse(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }


    void Update()
    {
        

        // Movement inputs in relation to camera
        Vector3 forwardRelativeMovementVector = cameraTransform.forward;
        Vector3 rightRelativeMovementVector = cameraTransform.right;

        // Normalize vectors
        forwardRelativeMovementVector.y = 0f;
        rightRelativeMovementVector.y = 0f;
        forwardRelativeMovementVector.Normalize();
        rightRelativeMovementVector.Normalize();

        // Calculate the movement direction
        Vector3 moveDirection = forwardRelativeMovementVector * moveInput.y + rightRelativeMovementVector * moveInput.x;


        // Change player orientation based on movement input
        controller.enabled = false;
        Quaternion targetRotation = moveDirection == Vector3.zero ? transform.rotation : Quaternion.LookRotation(moveDirection.normalized, Vector3.up);
        transform.rotation = targetRotation;
        controller.enabled = true;

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Movement Actions
        LedgeGrab();
        Crouching();
        Sprinting();
        CheckBounds();

        StartCoroutine("TimelineJump");

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    // Grab the ledge when falling
    void LedgeGrab()
    {
        if(_rb.linearVelocity.y < 0 && !hanging)
        {
            RaycastHit downHit;
            Vector3 lineDownStart = (transform.position + Vector3.up * 3f) + transform.forward;
            Vector3 lineDownEnd = (transform.position + Vector3.up * 1.5f) + transform.forward;
            Physics.Linecast(lineDownStart, lineDownEnd, out downHit, LayerMask.GetMask("Ground"));
            Debug.DrawLine(lineDownStart, lineDownEnd);

            if(downHit.collider != null)
            {
                RaycastHit fwdHit;
                Vector3 linefwdStart = new Vector3(transform.position.x, downHit.point.y -0.1f, transform.position.z);
                Vector3 linefwdEnd = new Vector3(transform.position.x, downHit.point.y -0.1f, transform.position.z) + transform.forward * 3f;
                Physics.Linecast(linefwdStart, linefwdEnd, out fwdHit, LayerMask.GetMask("Ground"));
                Debug.DrawLine(linefwdStart, linefwdEnd);

                if(fwdHit.collider != null)
                {
                    _rb.useGravity = false;
                    _rb.linearVelocity = Vector3.zero;

                    hanging = true;

                    Vector3 hangPos = new Vector3(fwdHit.point.x, downHit.point.y, fwdHit.point.z);
                    Vector3 offset = transform.forward * -0.1f + transform.up * -1f;
                    hangPos += offset;
                    transform.position = hangPos;
                    transform.forward = -fwdHit.normal;
                }
            }
        }
    }

    // Crouching calculations
    private void Crouching()
    {
        crouchScale = new Vector3(1, .7f, 1);
        standScale = new Vector3(1, 1, 1);

        if (isCrouching == true)
        {
            transform.localScale = crouchScale;
        }
        else if (isCrouching != true)
        {
            transform.localScale = standScale;
        }
    }

    // Spritning calculations
    private void Sprinting()
    {
        if (isSprinting != true)
        {
            moveSpeed = baseSpeed;
            _stamina += 3.0f * Time.deltaTime;
            
        }
        else if (isSprinting == true)
        {
            moveSpeed = sprintSpeed;
            _stamina -= 8.0f * Time.deltaTime;

            if (isSprinting == true && _stamina <= 0.1f)
            {
                moveSpeed = baseSpeed;
            }
        }
    }

    IEnumerator TimelineJump()
    {
        firstTimeline = new Vector3(transform.position.x - 500f, transform.position.y + 0f, transform.position.z + 0f);
        secondTimeline = new Vector3(transform.position.x + 500f, transform.position.y + 0f, transform.position.z + 0f);

        if (canTimeHop == true && timelineDif == true)
        {
            canTimeHop = false;
            timelineDif = false;
            yield return new WaitForSeconds(0.5f);
            Player.transform.localPosition = firstTimeline;

        }
        else if (canTimeHop == true && timelineDif != true)
        {
            canTimeHop = false;
            timelineDif = true;
            yield return new WaitForSeconds(0.5f);
            Player.transform.localPosition = secondTimeline;
        }
    }

    void CheckBounds()
    {
        if (transform.position.y < worldBottomBoundary)
        {
            var (position, rotation) = initialPositionAndRotation;
        }

    }


    private void LateUpdate()
    {
        CameraRotation();
    }

    void CameraRotation()
    {
        _pitchX += lookInput.x;
        _pitchY += lookInput.y;
        Quaternion rotation = Quaternion.Euler(_pitchX, _pitchY, 0);
        cameraTransform.rotation = rotation;
    } 
}
