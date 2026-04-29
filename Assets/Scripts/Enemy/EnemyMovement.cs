using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    // GameObject References
    private GameObject Player;
    private PlayerController player;

    // Transforms
    public Transform Target;

    // Vectors
    public Vector3 walkPoint;

    // Floats
    public float UpdateSpeed = 0.1f;
    public float walkPointRange;
    public float fovRange = 5f;
    private float chaseTimer = 7f;
    [SerializeField] private float walkTime;
    private float alertTimer = 0f;
    private float sightRange;


    // Booleans
    public bool walkPointSet;
    public bool playerInSightRange;
    bool chasing;
    
    // LayerMask

    public LayerMask whatIsPlayer, whatIsGround;

    // Navmesh
    private NavMeshAgent Agent;

    // Animation
    [SerializeField] private Animator animator;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Target = GameObject.Find("Player").transform;
    }

    private void Update()
    {

        Vector3 targetDir = Target.position - transform.position;
        Vector3 targetDist = (Target.position - transform.position).normalized;
        float fovDist = Vector3.Angle(targetDist * 1.5f, transform.position);
        float fovAngle = Vector3.Angle(targetDir, transform.position);
        // // Check for sight range
        // playerInSightRange = Physics.SphereCast(transform.position, sightRange, transform.forward, out hitinfo, 5f, whatIsPlayer);


        if (fovDist < 45)
        {
            playerInSightRange = true;
            return;
        } 

        if (!playerInSightRange)
        {  
            Patrolling();
        }
        if (playerInSightRange)
        {
            ChasePlayer();
            return;
        }

        walkTime += Time.deltaTime;
        alertTimer -= Time.deltaTime;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
    }

    // Walking area for enemy
    private void Patrolling()
    {
        
        Debug.Log("Patrolling");
        if (!walkPointSet || walkTime >= 6.0f)
        SearchWalkPoint();

        if (walkPointSet)
            Agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    // Calculate the range enemy can walk
    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
            EnemySoundManager.PlayOSSound(EnemySoundType.guard_walk, .2f);
            walkTime = 0f;
    }

    // Chase the player when walking into range
    private void ChasePlayer()
    { 
        Debug.Log("Chasing");
        // Chase Timer
        chaseTimer -= Time.deltaTime;
        alertTimer -= Time.deltaTime;

        // Chase Logic
        Agent.SetDestination(Target.position);
        Debug.Log("Player in sight");
    
            if (alertTimer == 0.1f)
            {
                EnemySoundManager.PlayOSSound(EnemySoundType.guard_alert);
                alertTimer = 6f;
                return;
            }
            if (Vector3.Distance(transform.position, Target.position) >= sightRange && chaseTimer <= 0)
            {
                chaseTimer = 7f;
                playerInSightRange = false;
                Patrolling();
            }
    }   

    public void OnTriggerEnter(Collider other)
    { 
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
    

        if (player != null)
        {
            EnemySoundManager.PlayOSSound(EnemySoundType.guard_attack);
            player.ChangeHealth(-10);

        }
    }
    
}