using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;
using System.Collections;


public class PlayerController : MonoBehaviour
{

    // Speed variables
    [SerializeField] private float gravity;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float doubleJumpHeight;
    private float moveSpeed;

    // Resource variables
    private float _stamina = 50f;
    public int maxHealth = 100;
    int currentHealth;
    public int health { get { return currentHealth; }}
    public int maxEnergy = 50;
    int currentEnergy;
    [SerializeField] private float BatteryCount;
    // [SerializeField] private float worldBottomBoundary = -100f;

    // Mouse movement variables
    private float _pitchX;
    private float _pitchY;
    private float _sensitivity;
    private Vector2 moveInput;
    private Vector2 lookInput;
    
    // Transforms
    [SerializeField] private Transform cameraTransform;
    public Transform timelineOne;
    public Transform timelineTwo;

    // Movement Vectors

    private Vector3 velocity;
    private Vector2 sprintInput;
    private Vector3 crouchScale;
    private Vector3 standScale;
    private Vector3 timeHopPos;
    private Vector3 firstTimeline;
    private Vector3 secondTimeline;
    private Vector3 vectorReset;
    // (Vector3, Quaternion) currentCheckpoint;
    
    // Reference to Player
    public GameObject Player;

    // Input Action variables
    private InputAction sprintAction;
    private bool isSprinting = false;
    bool hanging;
    private bool isCrouching;
    private bool canTimeHop;
    private bool timelineDif;
    private bool switchTimeline;
    private bool canDoubleJump;

    // Damage Logic variables
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    private int extraHits = 0;


    // Components
    private Rigidbody _rb;
    private CharacterController controller;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        // currentCheckpoint = (transform.position, transform.rotation);
    }

    // Player Movement
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        // Debug.Log($"Move Input: {moveInput}");
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
            if (hanging)
            {
                _rb.useGravity = true;
                hanging = false;

                velocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
                canDoubleJump = true;
            }
            else
            {
                velocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
                canDoubleJump = true;
            }
        }
        if (context.performed && controller.isGrounded != true && canDoubleJump == true)
        {
            velocity.y = Mathf.Sqrt(-2f * doubleJumpHeight * gravity);
            canDoubleJump = false;
        }
    }
    // Crouching
    public void OnCrouch(InputAction.CallbackContext context)
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

    public void OnItemUse(InputAction.CallbackContext context)
    {
        if (context.performed && BatteryCount >= 1f)
        {
            BatteryCount -= 1;
            currentEnergy += 25;
        }
    }

    // Time swap mechanic
    public void OnTimeSwap(InputAction.CallbackContext context)
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
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            
                isInvincible = false;
        }

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


        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        controller.enabled = false;
        Quaternion targetRotation = moveDirection == Vector3.zero ? transform.rotation : Quaternion.LookRotation(moveDirection.normalized, Vector3.up);
        transform.rotation = targetRotation;
        controller.enabled = true;

        // Movement Actions
        LedgeGrab();
        Crouching();
        Sprinting();
        StartCoroutine("TimelineJump");


        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    IEnumerator TimelineJump()
    {
        Vector3 firstTimeline = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 1500);
        Vector3 secondTimeline = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + -1500);

        if (canTimeHop == true)
        {
            if (timelineDif == true)
            {
            Debug.Log("first TimeLine");
            canTimeHop = false;
            yield return new WaitForSeconds(0.0f);
            controller.enabled = false;
            Player.transform.position = firstTimeline;
            controller.enabled = true;
            timelineDif = false;
            }
        else if (timelineDif == false)
            {
            Debug.Log("first TimeLine");
            canTimeHop = false;
            yield return new WaitForSeconds(0.0f);
            controller.enabled = false;
            Player.transform.position = secondTimeline;
            controller.enabled = true;
            timelineDif = true;
            }   
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            extraHits++;
        }
    }

    public void PlayerHit()
    {
        if (extraHits > 0)
        {
        // Prevent extra hits within a short amount of time
        extraHits--;
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;   
            }
                isInvincible = true;
                invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log("Current health: " + currentHealth);

    }

    private void ChangeEnergy(int EnergyAmount)
    {
        currentEnergy = Mathf.Clamp(currentEnergy + EnergyAmount, 0 , maxEnergy);
        Debug.Log("Current Energy: " + currentEnergy);
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
    
    public void CollectBattery(GameObject Battery)
    {
        BatteryCount += 1;
        Debug.Log("Collected Batteries: " + BatteryCount);
        Destroy(Battery);
    }
}
