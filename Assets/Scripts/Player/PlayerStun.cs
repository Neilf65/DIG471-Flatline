using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStun : MonoBehaviour
{
    // Takedown
   [SerializeField] private Transform enemyTransform;
   [SerializeField] EnemyTakedown enemyTakedown;
   private float turn_speed = 3f;

    // Reference to player
   public PlayerController playerController;

    // Animation
    [SerializeField] private Animator animator;

    // Input Actions
    private PlayerControls playerControls;
    private InputAction inputAction;


    private void Awake()
    {
        enemyTransform = GameObject.FindGameObjectWithTag("Enemy").transform;
    }
   private void Update()
    {
        
        Transform enemyTransform = enemyTakedown.transform;
        Vector3 dirFromEnemyToPlayer = ( transform.position - enemyTransform.position ).normalized;
        float dot = Vector3.Dot( enemyTransform.forward, dirFromEnemyToPlayer );

        

        float takedownDotOffset = .1f;
        float takedownDistance = 2f;
        if ( playerController.isInteracting != false && dot < -1 + takedownDotOffset  && Vector3.Distance(transform.position, enemyTransform.position) < takedownDistance)
            {
                // Takedown

                Vector3 dirToEnemy = ( enemyTransform.position - transform.position ).normalized;
                SetTargetForward(dirToEnemy);
                enemyTakedown.SetTargetForward(dirToEnemy);

                enemyTakedown.ForceMoveToPosition(transform.position + dirToEnemy * 1.5f);

            }
            else
            {
                
            }


        
    }

    private void SetTargetForward(Vector3 targetForward)
    {
        Quaternion _lookRotation = Quaternion.LookRotation((enemyTransform.position - transform.position).normalized);

        // over time
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * turn_speed);


    }
}
