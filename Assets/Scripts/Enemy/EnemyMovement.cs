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
    public Transform rayPoint;

    // Vectors
    public Vector3 walkPoint;
    public Vector3 lookDir = Vector3.down;

    // Floats
    public float UpdateSpeed = 0.1f;
    public float walkPointRange;
    public float sightRange;
    public float fovRange = 5f;
    [SerializeField] private float walkTime;


    // Booleans
    bool walkPointSet;
    public bool playerInSightRange;
    
    // LayerMask

    public LayerMask whatIsPlayer, whatIsGround;

    // Navmesh
    private NavMeshAgent Agent;

    // Audio 
    [SerializeField] private AudioClip alertSFX;



    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Target = GameObject.Find("Player").transform;
        rayPoint = gameObject.transform;
    }

    private void Update()
    {
        
        // Check for sight range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (!playerInSightRange) Patrolling();
        if (playerInSightRange) ChasePlayer();

        walkTime += Time.deltaTime;
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
            walkTime = 0f;
    }

    // Chase the player when walking into range
    private void ChasePlayer()
    {
        Agent.SetDestination(Target.position);
    }   

    public void OnTriggerEnter(Collider other)
    { 
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
    

        if (player != null)
        {
            player.ChangeHealth(-10);
        }
    }
}