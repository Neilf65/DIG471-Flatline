using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    // GameObject References
    private GameObject Player;

    // Transforms
    public Transform Target;

    // Vectors
    public Vector3 walkPoint;

    // Floats
    public float UpdateSpeed = 0.1f;
    public float walkPointRange;
    public float fovRange = 5f;
    [SerializeField] private float walkTime;
    private float alertTimer = 0f;


    // Booleans
    public bool walkPointSet;
    public bool playerInSightRange;
    
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
        float fovAngle = Vector3.Angle(targetDir, transform.position);
        // RaycastHit hitinfo;
        // // Check for sight range
        // playerInSightRange = Physics.SphereCast(transform.position, sightRange, transform.forward, out hitinfo, 5f, whatIsPlayer);

        if (fovAngle < 70)
        {
            playerInSightRange = true;
        }   
        if (playerInSightRange)
        {
            ChasePlayer();
        }
        else if (!playerInSightRange) 
        {
            Patrolling();
        }


        walkTime += Time.deltaTime;
        alertTimer -= Time.deltaTime;
    }

    // Walking area for enemy
    private void Patrolling()
    {
        if (!walkPointSet || walkTime >= 4.0f) 
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
            EnemySoundManager.PlayOSSound(EnemySoundType.guard_walk, .4f);
            walkTime = 0f;
    }

    // Chase the player when walking into range
    private void ChasePlayer()
    {
        Agent.SetDestination(Target.position);
        Debug.Log("Player in sight");
        if (alertTimer <= 0.1f){
        EnemySoundManager.PlayOSSound(EnemySoundType.guard_alert);
        alertTimer = 4f;
        return;
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