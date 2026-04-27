using UnityEngine;

public class EnemyTakedown : MonoBehaviour
{
    private Vector3 targetForward;
    private Vector3 forceMoveToPosition;

    private void Awake()
    {
        targetForward = transform.forward;
        forceMoveToPosition = transform.position;
    }

    private void Update()
    {
        HandleSmoothRotationForward();
        HandleSmoothForceMovement();
    }

    public void PlayTakedownAnimation()
    {
        
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

    public void ForceMoveToPosition(Vector3 forceMoveToPosition)
    {
        this.forceMoveToPosition = forceMoveToPosition;
    }

    public void SetTargetForward(Vector3 targetForward)
    {
        this.targetForward = targetForward;
    }
}
