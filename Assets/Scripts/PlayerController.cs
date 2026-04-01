using UnityEngine;
using UnityEngine.InputSystem;
using System;
using NUnit.Framework;


public class PlayerController : MonoBehaviour
{
    // Floats
    [SerializeField] private float gravity;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float _sensitivity;

    private float moveSpeed;
    private float _stamina = 50;
    private float _pitchX;
    private float _pitchY;
    
    // Transforms
    [SerializeField] private Transform cameraTransform;

    // Vectors
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private Vector2 sprintInput;

    private InputAction sprintAction;
    private bool isSprinting = false;
    bool hanging;


    // Components
    private Rigidbody _rb;
    private CharacterController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    // Move on input
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log($"Move Input: {moveInput}");
    }

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

    // Jump on input
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

    public void onCrouch(InputAction.CallbackContext context)
    {
        
    }

    // Move camera on input
    public void OnMouse(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }


    // Update is called once per frame
    void Update()
    {
        // Grab ledge when airborne
        LedgeGrab();

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

        Debug.Log ("Current stamina: " + _stamina);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

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
