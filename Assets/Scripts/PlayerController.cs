using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    // Variables

    // Gravity
    [SerializeField] private float gravity;
    private float moveSpeed;

    // Movement
    [SerializeField] private float baseSpeed;
    private Vector3 velocity;
    private Vector3 moveDirection;

    // Sprinting
    [SerializeField] private float sprintSpeed;
    // Jumping
    [SerializeField] private float jumpHeight;
    [SerializeField] private float doubleJumpHeight;

    // Dashing
    public const float maxDashTime = 1.0f;
    public float dashDistance = 10;
    float dashStoppingSpeed = 0.1f;
    float currentDashTime = maxDashTime;
    float dashSpeed = 6;

    // Crouching
    private Vector3 crouchScale;
    private Vector3 standScale;

    // Health
    public int maxHealth = 100;
    public int currentHealth;
    public int health { get { return currentHealth; }}

    // Stamina 
    public float _stamina = 50f;
    public float currentStamina;

    // Energy 
    int currentEnergy;
    public int maxEnergy = 50;
    private float BatteryCount;

    // Mouse movement
    private float _pitchX;
    private float _pitchY;
    private Vector2 moveInput;
    private Vector2 lookInput;
    
    // Camera
    [SerializeField] private Transform cameraTransform;

    // Timeline

    
    // Player
    public GameObject Player;

    // Input Action Bools
    private bool isSprinting = false;
    bool hanging;
    private bool isCrouching;
    private bool canTimeHop;
    private bool timelineDif;
    private bool canDoubleJump;
    private bool dashNow = false;
    public bool isInteracting = false;

    // Damage Logic
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    private int extraHits = 0;

    // UI
    public EnergyBar energyBar;


    // Components
    private Rigidbody _rb;
    private CharacterController controller;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
        currentStamina = _stamina;
    }

    // Player Movement
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        // Debug.Log($"Move Input: {moveInput}");
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log($"Interacting {context.performed}");
            isInteracting = true;
        }
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

    // Dashing
    public void OnDash(InputAction.CallbackContext context)
    {
        Debug.Log($"Dashing {context.performed}");
        if (context.performed)
        {
            dashNow = true;
        }
        else
        {
            dashNow = false;
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
                _rb.useGravity = false;
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
        if (context.performed && controller.isGrounded != true && canDoubleJump == true && currentStamina >= 5)
        {
            velocity.y = Mathf.Sqrt(-2f * doubleJumpHeight * gravity);
            canDoubleJump = false;
            ChangeStamina(-5);
            Debug.Log("current stamina" + currentStamina);
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
        if (context.performed && BatteryCount >= 1f && currentEnergy < 50)
        {
            BatteryCount -= 1;
            ChangeEnergy(25);
            Debug.Log("Current batteries: " + BatteryCount);
            Debug.Log("Current energy: " + currentEnergy);
        }
        if (context.performed && currentEnergy == maxEnergy)
        {
        
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
        Dashing();
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
            Vector3 lineDownStart = (transform.position + Vector3.up * 1f) + transform.forward;
            Vector3 lineDownEnd = (transform.position + Vector3.up * .5f) + transform.forward;
            Physics.Linecast(lineDownStart, lineDownEnd, out downHit, LayerMask.GetMask("Ground"));
            Debug.DrawLine(lineDownStart, lineDownEnd);
            Debug.Log(downHit.transform.name);

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
                    controller.enabled = false;
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

            if (currentStamina < 50)
            {
                currentStamina += 3.0f * Time.deltaTime;
            }
        }
        else if (isSprinting == true)
        {
            moveSpeed = sprintSpeed;
            currentStamina -= 8.0f * Time.deltaTime;

            if (isSprinting == true && currentStamina <= 0.1f)
            {
                moveSpeed = baseSpeed;
                isSprinting = false;
            }
        }
        // Debug.Log("Current Stamina: " + currentStamina);
    }

    public void Dashing()
    {   
        if (dashNow == true && currentEnergy >= 10)
        {
            
            ChangeEnergy(-10);
            Debug.Log("current energy" + currentEnergy);
            currentDashTime = 0;
        }
        if (currentDashTime < maxDashTime)
        {
            moveDirection = transform.forward * dashDistance;
            currentDashTime += dashStoppingSpeed;
            Debug.Log (currentDashTime);
            controller.Move(velocity * Time.deltaTime);
        }
        else 
        {
            moveDirection = Vector3.zero;
        }
        controller.Move(moveDirection * Time.deltaTime * dashSpeed);
        dashNow = false;
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
        // energyBar.SetEnergy(currentEnergy);
    }

    private void ChangeStamina(float stamAmount)
    {
        currentStamina = Mathf.Clamp(currentStamina + stamAmount, 0, _stamina);

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
