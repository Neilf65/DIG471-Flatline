using UnityEngine;
using UnityEngine.AI;

public class EnemyTakedown : MonoBehaviour
{
    [SerializeField] private Transform enemyTransform;
    private Vector3 targetForward;
    private Vector3 forceMoveToPosition;
    private PlayerStun playerStun;
    [SerializeField] PlayerController playerController;
    [SerializeField] EnemyMovement enemyMovement;
    public bool takeDown;
    float takeDownTimer;

    private void Awake()
    {
        
    }

    private void Update()
    {
        targetForward = transform.forward;
        forceMoveToPosition = transform.position;
        Vector3 targetDist = enemyTransform.position - transform.position;

        float takeDownDist = 3f;
        if (playerController.takeDown && Vector3.Distance(transform.position, enemyTransform.position) < takeDownDist)
        {
            takeDownTimer -= 5f;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            Debug.Log("Should takeDown now");

            {
                PlayTakeDownAnimation();
            }

        }

    }

    public void PlayTakeDownAnimation()
    {
        takeDownTimer -= Time.deltaTime;;
    
        HandleSmoothRotationForward();
        HandleSmoothForceMovement();
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
