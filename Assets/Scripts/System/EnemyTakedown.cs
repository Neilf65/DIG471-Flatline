using UnityEngine;

public class EnemyTakedown : MonoBehaviour
{
    private Vector3 targetForward;
    private Vector3 forceMoveToPosition;
    private PlayerStun playerStun;
    [SerializeField] PlayerController playerController;
    public bool takeDown;
    float takeDownTimer;

    private void Awake()
    {
        
    }

    private void Update()
    {
        targetForward = transform.forward;
        forceMoveToPosition = transform.position;
        if (playerController.takeDown)
        {
            takeDownTimer -= 5f;
            {
                PlayTakeDownAnimation();
            }

        }

    }

    public void PlayTakeDownAnimation()
    {
        takeDownTimer -= Time.deltaTime;;
    
        HandleSmoothRotationForward();
        // HandleSmoothForceMovement();
        takeDownTimer = 5f;
        if (takeDownTimer <= 0)
            {
                Destroy(gameObject);
            }
        Debug.Log("Enemy takedown: " + takeDown);
    }

    private void HandleSmoothRotationForward()
    {
        float rotationSpeed = 10f;
        transform.forward = Vector3.Lerp(transform.forward, targetForward, Time.deltaTime * rotationSpeed);
    }

    private void HandleSmoothForceMovement()
    {
        float moveSpeed = 10f;
        transform.position = Vector3.Lerp(transform.position, forceMoveToPosition, Time.deltaTime * moveSpeed);
    }
    public void SetTargetForward(Vector3 targetForward)
    {
        this.targetForward = targetForward;
    }

    public void ForceMoveToPosition(Vector3 forceMoveToPosition)
    {
        this.forceMoveToPosition = forceMoveToPosition;
    }

}
